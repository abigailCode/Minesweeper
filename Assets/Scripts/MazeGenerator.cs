using UnityEngine;
public class MazeGenerator : MonoBehaviour
{
    public int width = 10; // Ancho del laberinto
    public int height = 10; // Altura del laberinto
    private int[,] maze; // Representaci�n del laberinto en un array bidimensional
    public GameObject wallPrefab; // Asigna tu prefab de muro desde el Inspector de Unity
    public GameObject pathPrefab;
    public GameObject[][] map;

    void Start()
    {
        map = new GameObject[width][];
        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new GameObject[height];
        }
        GenerateMaze();
        PrintMaze();


        Camera.main.transform.position =
            new Vector3(((float)width / 2) - 0.5f, ((float)height / 2) - 0.5f, -10);
    }
    void GenerateMaze()
    {
        maze = new int[width, height];
        // Inicializar el laberinto
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                maze[i, j] = 1; // 1 representa un muro, 0 representa un camino
            }
        }
        // Llamar al m�todo de generaci�n recursiva
        GeneratePath(1, 1);
        // Puedes imprimir el laberinto en la consola para ver la representaci�n
      
    }
    void GeneratePath(int x, int y)
    {
        maze[x, y] = 0; // Marcar la posici�n actual como parte del camino
                        // Direcciones posibles (arriba, derecha, abajo, izquierda)
        int[] directions = { 0, 1, 2, 3 };
        Shuffle(directions);
        // Explorar direcciones posibles
        for (int i = 0; i < directions.Length; i++)
        {
            int nextX = x + 2 * (directions[i] == 1 ? 1 : (directions[i] == 3 ? -1 : 0));
            int nextY = y + 2 * (directions[i] == 2 ? 1 : (directions[i] == 0 ? -1 : 0));
            // Verificar si la pr�xima posici�n est� dentro de los l�mites y a�n no ha sido visitada
            if (nextX > 0 && nextX < width - 1 && nextY > 0 && nextY < height - 1 && maze[nextX, nextY] == 1)
            {
                maze[x + (nextX - x) / 2, y + (nextY - y) / 2] = 0; // Romper el muro entre las posiciones
                GeneratePath(nextX, nextY);
            }
        }
    }
    void Shuffle(int[] array)
    {
        // M�todo para barajar un array
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
  void PrintMaze()
{
    float verticalSpacing = 1f; // Ajusta este valor para la separaci�n vertical deseada

    for (int j = height - 1; j >= 0; j--)
    {
        for (int i = 0; i < width; i++)
        {
            if (maze[i, j] == 1)
            {
                map[i][j] = Instantiate(wallPrefab, new Vector3(i, j * verticalSpacing, 0), Quaternion.identity);
                Debug.Log("#"); // Muro
            }
            else
            {
                map[i][j] = Instantiate(pathPrefab, new Vector3(i, j * verticalSpacing, 0), Quaternion.identity);
                Debug.Log(" "); // Camino
            }
        }
        Debug.Log("\n");
    }
}


}