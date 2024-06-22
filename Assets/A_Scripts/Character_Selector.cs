using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public GameObject characterItemPrefab; // Prefab của Item chứa nhân vật
    public GameObject renderCameraPrefab;  // Prefab của Camera render
    public Transform contentPanel;         // Panel chứa các Item
    public GameObject[] characterPrefabs;  // Array chứa các prefab nhân vật 3D
    public Vector3 startSpawnPosition;     // Vị trí ban đầu cho việc đặt nhân vật
    public float offsetBetweenCharacters = 2f; // Khoảng cách giữa các nhân vật
    public Vector3 offsetBetweenCamEndChar = new Vector3(0, 0, 0);
    public Text textAllCharacter;

    private Vector3 currentSpawnPosition;

    void Start()
    {
        // Đếm số lượng characterPrefabs
        int characterCount = characterPrefabs.Length;
        Debug.Log("Number of characters: " + characterCount);

        textAllCharacter.text = "/" + characterCount.ToString();

        currentSpawnPosition = startSpawnPosition;
        foreach (var characterPrefab in characterPrefabs)
        {
            AddCharacterItem(characterPrefab);
            currentSpawnPosition += new Vector3(offsetBetweenCharacters, 0f, 0f);
        }
    }

    void AddCharacterItem(GameObject characterPrefab)
    {
        // Tạo một instance mới từ prefab
        GameObject newItem = Instantiate(characterItemPrefab, contentPanel);

        // Tạo Render Texture cho nhân vật
        RenderTexture renderTexture = new RenderTexture(1080, 1920, 32);

        // Tạo nhân vật trong Scene và đặt nó ở vị trí cụ thể
        GameObject characterInstance = Instantiate(characterPrefab, currentSpawnPosition, Quaternion.identity);
        characterInstance.SetActive(true);

        // Thiết lập characterInstance làm con của newItem
        characterInstance.transform.SetParent(newItem.transform, false);

        // Tạo một camera render riêng cho nhân vật này
        GameObject cameraInstance = Instantiate(renderCameraPrefab);
        Camera renderCamera = cameraInstance.GetComponent<Camera>();
        renderCamera.targetTexture = renderTexture;

        // Thiết lập cameraInstance làm con của newItem
        cameraInstance.transform.SetParent(newItem.transform, false);

        // Thiết lập vị trí của Camera để nhìn vào nhân vật
        renderCamera.transform.position = characterInstance.transform.position + offsetBetweenCamEndChar;
        //renderCamera.transform.LookAt(characterInstance.transform);
        renderCamera.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        // Gán Render Texture cho RawImage trong Item
        RawImage rawImage = newItem.GetComponentInChildren<RawImage>();
        rawImage.texture = renderTexture;

        // Cập nhật vị trí spawn cho nhân vật tiếp theo
        currentSpawnPosition += new Vector3(offsetBetweenCharacters, 0f, 0f);
    }
}