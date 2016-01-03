using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

namespace MSoft.AsyncResponces
{
    /// <summary>
    /// Определяет базовый класс для клиента web api.
    /// </summary>
    public class WebApiClient : HttpClient
    {
        private void InitializeClient()
        {
            settings = new Lazy<JsonSerializerSettings>(InitializeSettings);
            serializer = new Lazy<JsonSerializer>(InitializeSerializer);
            formatters = new Lazy<IEnumerable<JsonMediaTypeFormatter>>(InitializeFormatters);
        }

        private JsonSerializer InitializeSerializer()
        {
            var settings = InitializeSettings();

            return JsonSerializer.Create(settings);
        }

        private JsonSerializerSettings InitializeSettings()
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Newtonsoft.Json.Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return settings;
        }

        private IEnumerable<JsonMediaTypeFormatter> InitializeFormatters()
        {
            return new List<JsonMediaTypeFormatter> { new JsonMediaTypeFormatter() { SerializerSettings = InitializeSettings() } };
        }

        #region Protected

        protected readonly TimeSpan defaultWaitTime = TimeSpan.FromSeconds(10);
        protected readonly TimeSpan defaultLapTime = TimeSpan.FromSeconds(1);

        /// <summary>
        /// Возвращает результат отклика.
        /// </summary>
        /// <typeparam name="T">Тип результата отклика.</typeparam>
        /// <param name="requestString">Путь к апи.</param>
        /// <param name="waitTime">Время ожидания.</param>
        /// <param name="lapTime">Время круга.</param>
        /// <param name="onLapAction">Действие при повторении</param>
        /// <returns></returns>
        protected virtual async Task<T> GetResponceResult<T>(string requestString, TimeSpan waitTime, TimeSpan lapTime, Action onLapAction)
            where T:class
        {
            var currentWaitTime = new TimeSpan();

            while(true)
            {
                var response = await GetResponceResult<T>(requestString);                

                if(response != null)
                {
                    return response;
                }
                
                if(currentWaitTime == waitTime)
                {
                    return null;
                }

                currentWaitTime += lapTime;

                onLapAction();
            }
        }

        /// <summary>
        /// Возвращает результат отклика.
        /// </summary>
        /// <typeparam name="T">Тип результата отклика.</typeparam>
        /// <param name="requestString">Путь к апи.</param>
        /// <returns>Отклик запроса.</returns>
        protected async Task<T> GetResponceResult<T>(string requestString)
        {
            try
            {
                var response = await GetAsync(requestString);

                T result = default(T);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result = await response.Content.ReadAsAsync<T>(Formatters);
                }

                return result;
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }

        /// <summary>
        /// Возвращает результат отклика.
        /// </summary>
        /// <typeparam name="T">Тип результата отклика.</typeparam>
        /// <param name="requestString">Путь к апи.</param>
        /// <returns>Отклик запроса.</returns>
        protected async Task<T> GetResponceResult<T>(string requestString, params object[] args)
        {
            return await GetResponceResult<T>(string.Format(requestString, args));
        }

        /// <summary>
        /// Выполняет метод POST и возвращает его результат.
        /// </summary>
        /// <typeparam name="TResult">Тип результата отклика.</typeparam>
        /// <typeparam name="TModel">Тип модели используемой в POST.</typeparam>
        /// <param name="requestString">Путь к апи.</param>
        /// <param name="model">Модель.</param>
        /// <returns>Результат выполнения метода POST.</returns>
        protected async Task<TResult> PostAsync<TResult, TModel>(string requestString, TModel model)
        {
            try
            {

                var post = await this.PostAsJsonAsync(requestString, model);

                var result = default(TResult);

                if (post.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result = await post.Content.ReadAsAsync<TResult>(Formatters);
                }

                return result;
            }
            catch (HttpResponseException)
            {
                return default(TResult);
            }
        }    

        /// <summary>
        /// Выполняет метод PUT и возвращает его результат.
        /// </summary>
        /// <typeparam name="TResult">Тип результата отклика.</typeparam>
        /// <typeparam name="TModel">Тип модели используемой в PUT.</typeparam>
        /// <param name="requestString">Путь к апи.</param>
        /// <param name="model">Модель.</param>
        /// <returns>Результат выполнения метода PUT.</returns>
        protected async Task<TResult> PutAsync<TResult, TModel>(string requestString, TModel model)
        {
            try
            {
                var put = await this.PutAsJsonAsync(requestString, model);

                TResult result = default(TResult);

                if (put.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result = await put.Content.ReadAsAsync<TResult>(Formatters);
                }

                return result;
            }
            catch (HttpResponseException)
            {
                return default(TResult);
            }
        }

        protected JsonSerializer Serializer
        {
            get
            {
                return serializer.Value;
            }
        }
        private Lazy<JsonSerializer> serializer;

        protected JsonSerializerSettings Settings
        {
            get
            {
                return settings.Value;
            }
        }
        private Lazy<JsonSerializerSettings> settings;

        protected IEnumerable<JsonMediaTypeFormatter> Formatters
        {
            get
            {
                return formatters.Value;
            }
        }
        private Lazy<IEnumerable<JsonMediaTypeFormatter>> formatters;
 
	    #endregion

        #region Public

        public WebApiClient()
        {
            InitializeClient();
        }

        #endregion        
    }
}
