using System;

public class TTTAI
{
    private int moveCount = 0;
    private int n = 0;
    private int a = 0;
    private int lastMove = 0;
    private char characterByComputer;
    private char characterByPlayer;

    public TTTAI(char ch)
    {
        characterByPlayer = ch;
        characterByComputer = (ch == 'X') ? 'O' : 'X';
    }

    public void Reset()
    {
        moveCount = 0;
        lastMove = 0;
        n = 0;
        a = 0;
    }

    private int Tools(int k)
    {
        if (k == 1 || k == 2 || k == 3)
            return 0;
        else if (k == 4)
            return 1;
        else if (k == 5 || k == 6)
            return 2;
        else if (k == 7)
            return 3;
        else
            return 6;
    }

    private int Tools1(int k)
    {
        if (k == 1)
            return 4;
        else if (k == 2 || k == 7 || k == 8)
            return 1;
        else if (k == 3 || k == 4 || k == 6)
            return 3;
        else
            return 2;
    }

    private int Tools3(char[] play, int m)
    {
        int z = PairCheck(play, m);
        if (z != -1)
            return z;

        z = Random(play);
        return z;
    }

    private int Move1(char[] play, int n)
    {
        if (play[4] == ' ' && n == 2)
        {
            moveCount++;
            return 4;
        }
        else if (n == 1 || play[4] != ' ')
        {
            moveCount++;
            return 2;
        }
        return 2;
    }

    private int PairCheck(char[] play, int n)
    {
        string encode = "";
        for (int k = 1; k <= 8; k++)
        {
            int x = Tools(k);
            int c = Tools1(k);
            int sum = 0;
            int sum1 = 0;
            int y = 0;
            int f = 0;
            char previousCharacter = '@';
            while (f != 3)
            {
                sum += x;
                previousCharacter = (y == 0) ? play[x] : previousCharacter;
                if (play[x] != ' ' && play[x] == previousCharacter)
                {
                    sum1 += x;
                    y++;
                }
                f++;
                x += c;
            }
            sum1 = sum - sum1;
            if (y == 2 && play[sum1] == ' ')
                encode += previousCharacter + sum1.ToString();
        }
        int l = encode.Length;
        if (l > 1)
        {
            int last = 0;
            for (int i = 0; i < l; i += 2)
            {
                if (encode[i] == characterByComputer)
                    return int.Parse(encode[i + 1].ToString());
                else
                    last = i;
            }
            return int.Parse(encode[last + 1].ToString());
        }
        return -1;
    }

    private int WinCase1(char[] play)
    {
        if (lastMove == 0 || lastMove == 2 || lastMove == 6 || lastMove == 8)
        {
            for (int i = 0; i <= 8; i += 2)
            {
                if (play[i] == ' ' && i != 4)
                    return i;
            }
        }
        return -1;
    }

    public int Random(char[] play)
    {
        int b = 0;
        int[] rand = new int[9];
        for (int z = 0; z < 9; z++)
            rand[z] = -1;

        b = 0;
        for (int i = 0; i < 9; i++)
        {
            if (play[i] == ' ')
            {
                rand[b] = i;
                b++;
            }
        }
        Random random = new Random();
        int j = random.Next(0, b);
        moveCount++;
        return rand[j];
    }

    public int Input(char[] play, int n, int x)
    {
        if (n == 2)
            return Second(play, n);
        else
            return First(play, n, x);
    }

    private int First(char[] play, int m, int x)
    {
        if (moveCount == 0)
            return Move1(play, m);
        else if (moveCount > 1 && n == 2)
            return Tools3(play, m);
        else if (play[4] == ' ' || n == 1)
        {
            n = 1;
            moveCount++;
            return FWin1(play, x);
        }
        else if (play[4] != ' ')
        {
            n = 2;
            moveCount++;
            return 6;
        }
        return 0;
    }

    private int FWin1(char[] play, int x)
    {
        if (moveCount == 2)
        {
            while (play[a] != characterByPlayer)
                a++;

            if (a == 0 || a == 1 || a == 7)
                return 8;
            else
                return 0;
        }
        else if (moveCount == 3)
        {
            n = 2;
            if (a == 0 || a == 1)
            {
                if (x == 5)
                    return 6;
                else
                    return 5;
            }
            else if (a == 3 || a == 6)
            {
                if (x == 1)
                    return 8;
                else
                    return 1;
            }
            else if (a == 5 || a == 8)
            {
                if (x == 1)
                    return 6;
                else
                    return 1;
            }
            else
            {
                if (x == 5)
                    return 0;
                else
                    return 5;
            }
        }
        return 0;
    }

    private int Second(char[] play, int n)
    {
        if (moveCount == 0)
        {
            lastMove = Move1(play, n);
            return lastMove;
        }
        else if (moveCount == 1 && play[4] == characterByComputer)
        {
            if (play[0] == characterByPlayer && play[8] == characterByPlayer && play[3] == ' ')
            {
                moveCount++;
                return 3;
            }
            else if (play[2] == characterByPlayer && play[6] == characterByPlayer && play[3] == ' ')
            {
                moveCount++;
                return 3;
            }
            else if (play[1] == characterByPlayer && play[7] == characterByPlayer && play[0] == ' ')
            {
                moveCount++;
                return 0;
            }
            else if (play[3] == characterByPlayer && play[5] == characterByPlayer && play[0] == ' ')
            {
                moveCount++;
                return 0;
            }
        }

        int z = PairCheck(play, n);
        if (z != -1)
            return z;

        if (moveCount == 1)
        {
            z = WinCase1(play);
            if (z != -1)
            {
                moveCount++;
                return z;
            }
        }

        z = Random(play);
        return z;
    }
}  