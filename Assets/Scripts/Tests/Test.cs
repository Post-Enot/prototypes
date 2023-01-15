using IUP.Toolkits.Matrices;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        TestMatrixForEachElements();
    }

    private void TestMatrixForEachElements()
    {
        Matrix<string> matrix = new(10, 10);
        matrix.InitAllElements((int x, int y) => "x");
        matrix.ForEachElements((ref string element, int x, int y) => element = "y");
        PrintMatrix(matrix);
    }

    private void TestMatrixResize()
    {
        TestT(3, 3, 1, 1, WidthResizeRule.Left, HeightResizeRule.Up);
        TestT(3, 3, -3, -3, WidthResizeRule.Left, HeightResizeRule.Up);

        TestT(3, 3, 1, 1, WidthResizeRule.Right, HeightResizeRule.Down);
        TestT(3, 3, -1, -1, WidthResizeRule.Right, HeightResizeRule.Down);

        TestT(3, 3, 1, 1, WidthResizeRule.CenterLeft, HeightResizeRule.CenterUp);
        TestT(3, 3, -1, -1, WidthResizeRule.CenterLeft, HeightResizeRule.CenterUp);

        TestT(3, 3, 1, 1, WidthResizeRule.CenterRight, HeightResizeRule.CenterDown);
        TestT(3, 3, -1, -1, WidthResizeRule.CenterRight, HeightResizeRule.CenterUp);
    }

    private void TestT(
        int startWidth,
        int startHeight,
        int xOffset,
        int yOffset,
        WidthResizeRule widthOffsetRule,
        HeightResizeRule heightOffsetRule)
    {
        Matrix<int> matrix = new(startWidth, startHeight);
        matrix.InitAllElements((int x, int y) => x + y);
        PrintResizedMatrix(matrix, widthOffsetRule, heightOffsetRule);
        matrix.Resize(xOffset, yOffset, widthOffsetRule, heightOffsetRule);
        PrintResizedMatrix(matrix, widthOffsetRule, heightOffsetRule);
    }

    private void PrintResizedMatrix(Matrix<int> matrix, WidthResizeRule widthOffsetRule, HeightResizeRule heightOffsetRule)
    {
        string str = $"width: {widthOffsetRule}; height: {heightOffsetRule}\n";
        for (int y = 0; y < matrix.Height; y += 1)
        {
            for (int x = 0; x < matrix.Width; x += 1)
            {
                str += matrix[x, y].ToString();
            }
            str += "\n";
        }
        Debug.Log(str);
    }

    private void PrintMatrix<T>(Matrix<T> matrix)
    {
        string str = "";
        for (int y = 0; y < matrix.Height; y += 1)
        {
            for (int x = 0; x < matrix.Width; x += 1)
            {
                str += matrix[x, y].ToString();
            }
            str += "\n";
        }
        Debug.Log(str);
    }
}
