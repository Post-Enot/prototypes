using IUP.Toolkits.Matrices;
using UnityEngine;

public class MatrixTest : MonoBehaviour
{
    private void Awake()
    {
        int width = 2;
        int height = 2;
        var matrix = new Matrix<int>(width, height);
        int i;
        i = 0;
        matrix.ForEachElements((ref int element) => element = i++);
        PrintMatrix(matrix);
        matrix.Mirror(MatrixMirror.Horizontal);
        PrintMatrix(matrix);

        i = 0;
        matrix.ForEachElements((ref int element) => element = i++);
        matrix.Mirror(MatrixMirror.Vertical);
        PrintMatrix(matrix);
    }

    private void TestMatrix(int width, int height, MatrixRotation rotation)
    {
        var matrix = new Matrix<int>(width, height);
        int i = 0;
        matrix.ForEachElements((ref int element) => element = i++);
        matrix.Rotate(rotation);
        PrintMatrix(matrix);
    }

    private void PrintMatrix<T>(Matrix<T> matrix)
    {
        string str = "";
        for (int y = 0; y < matrix.Height; y += 1)
        {
            string line = "";
            for (int x = 0; x < matrix.Width; x += 1)
            {
                line += matrix[x, y].ToString();
            }
            str = line + "\n" + str;
        }
        Debug.Log(str);
    }
}
