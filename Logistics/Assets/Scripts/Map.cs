using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Globalization;

public class Map : MonoBehaviour
{
    private string apiKeyStatic = "8605641e-f9f4-4b93-a64e-5dd8be5609ff";
    private string apiKeyGeoCode = "9c8b6eb4-caab-4dbd-abdc-441f9f88a071";
    private float longitude0 = 45.97252f;
    private float latitude0 = 51.57604f;
    private float longitude1 = 45.97515f;
    private float latitude1 = 51.58505f;
    private int zoom = 16;
    public RawImage mapRenderer;
    [SerializeField] InputField addres;

    void Start()
    {
        //StartCoroutine(LoadMap());
    }

    public void ShowMap(){
        StartCoroutine(GetCoordinatesByAddress(addres.text));
    }
    public void ShowMapForRequest(Request request){
        StartCoroutine(GetCoordinates(request.addressFrom, 0));
        StartCoroutine(GetCoordinates(request.addressTo, 1));
        //StartCoroutine(LoadMap());
    }
    public void ChangeZoom(int x){
        if(zoom + x <= 0 || zoom + x >= 20) return;
        zoom += x;
        StartCoroutine(LoadMap());
    }

    IEnumerator GetCoordinates(string targetAddress, int id){
        // Кодируем адрес для URL (заменяем пробелы на %20)
        string encodedAddress = UnityWebRequest.EscapeURL(targetAddress);
        
        // Формируем URL для запроса к Яндекс.Геокодеру
        string url = $"https://geocode-maps.yandex.ru/v1?apikey={apiKeyGeoCode}&geocode={encodedAddress}&format=json";
        
        // Отправляем GET-запрос
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        // Обработка ответа
        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            
            // Парсим JSON-ответ (самый простой способ без внешних библиотек)
            try
            {
                // Ищем поле "pos" в JSON, содержащее координаты
                int posIndex = jsonResponse.IndexOf("\"pos\":\"");
                if (posIndex != -1)
                {
                    // Извлекаем подстроку с координатами (формат "долгота широта")
                    string coordsStr = jsonResponse.Substring(posIndex + 7, 20).Split('\"')[0];
                    string[] coords = coordsStr.Split(' ');
                    
                    if (coords.Length >= 2)
                    {
                        if(id == 0) {
                            longitude0 = float.Parse(coords[0], CultureInfo.InvariantCulture);
                            latitude0 = float.Parse(coords[1], CultureInfo.InvariantCulture);
                            Debug.Log($"Координаты адреса '{targetAddress}': {longitude0}, {latitude0}");
                            StartCoroutine(LoadMap());
                        } else {
                            longitude1 = float.Parse(coords[0], CultureInfo.InvariantCulture);
                            latitude1 = float.Parse(coords[1], CultureInfo.InvariantCulture);
                            Debug.Log($"Координаты адреса '{targetAddress}': {longitude1}, {latitude1}");
                            StartCoroutine(LoadMap());
                        }
                    }
                }
                else
                {
                    Debug.LogError("Координаты не найдены в ответе API.");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Ошибка парсинга JSON: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("Ошибка геокодирования: " + request.error);
        }
    }

    IEnumerator GetCoordinatesByAddress(string targetAddress)
    {
        // Кодируем адрес для URL (заменяем пробелы на %20)
        string encodedAddress = UnityWebRequest.EscapeURL(targetAddress);
        
        // Формируем URL для запроса к Яндекс.Геокодеру
        string url = $"https://geocode-maps.yandex.ru/v1?apikey={apiKeyGeoCode}&geocode={encodedAddress}&format=json";
        
        // Отправляем GET-запрос
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        // Обработка ответа
        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            
            // Парсим JSON-ответ (самый простой способ без внешних библиотек)
            try
            {
                // Ищем поле "pos" в JSON, содержащее координаты
                int posIndex = jsonResponse.IndexOf("\"pos\":\"");
                if (posIndex != -1)
                {
                    // Извлекаем подстроку с координатами (формат "долгота широта")
                    string coordsStr = jsonResponse.Substring(posIndex + 7, 20).Split('\"')[0];
                    string[] coords = coordsStr.Split(' ');
                    
                    if (coords.Length >= 2)
                    {
                        longitude0 = float.Parse(coords[0], CultureInfo.InvariantCulture);
                        latitude0 = float.Parse(coords[1], CultureInfo.InvariantCulture);
                        
                        Debug.Log($"Координаты адреса '{targetAddress}': {longitude0}, {latitude0}");
                        
                        // Теперь можно использовать координаты для загрузки карты
                        StartCoroutine(LoadMap());
                    }
                }
                else
                {
                    Debug.LogError("Координаты не найдены в ответе API.");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Ошибка парсинга JSON: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("Ошибка геокодирования: " + request.error);
        }
    }

    IEnumerator LoadMap()
    {
        string lonStr0 = longitude0.ToString(CultureInfo.InvariantCulture);
        string latStr0 = latitude0.ToString(CultureInfo.InvariantCulture);

        string url = $"https://static-maps.yandex.ru/v1?ll={lonStr0},{latStr0}&lang=ru_RU&size=450,450&z={zoom}&apikey={apiKeyStatic}";
        url += $"&pt={lonStr0},{latStr0},pmwtm1";
        
        string lonStr1 = longitude1.ToString(CultureInfo.InvariantCulture);
        string latStr1 = latitude1.ToString(CultureInfo.InvariantCulture);
        url += $"~{lonStr1},{latStr1},pmwtm2";
        
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D mapTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            mapRenderer.texture = mapTexture;
        }
        else
        {
            Debug.LogError("Ошибка загрузки карты: " + request.error + "\nURL: " + url);
        }
    }
}