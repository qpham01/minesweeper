namespace ConsoleView
{
    public interface IConsoleGridView
    {
        void RenderMap(char[,] playerMap, int markRow, int markColumn);
    }
}
