using System;
using UnityEngine;

public class TTTAI
{
    int moveCount = 0; // Only counts moves by TTTAI, not the player
    int n = 0;
    int playersLastMove = 0;
    int firstMove;
    char characterByComputer;
    char characterByPlayer;
    int playersFirstMove = -1;
    char preferredCharacter;
    int[] corner = { 0, 2, 6, 8 };

    public TTTAI(char ch)
    {
        characterByPlayer = ch;
        characterByComputer = (ch == 'X') ? 'O' : 'X';
        preferredCharacter = characterByComputer;
        firstMove = 0;
    }

    /*public void Reset1()
    {
        moveCount = 0;
        playersLastMove = 0;
        n = 0;
    }*/

    int Tools(int k)
    {
        if (k == 1 || k == 2 || k == 3) return 0;
        if (k == 4) return 1;
        if (k == 5 || k == 6) return 2;
        if (k == 7) return 3;
        return 6;
    }

    int Tools1(int k)
    {
        if (k == 1) return 4;
        if (k == 2 || k == 7 || k == 8) return 1;
        if (k == 3 || k == 4 || k == 6) return 3;
        return 2;
    }

    bool CornerCheck(int move)
    {
        foreach (int c in corner)
        {
            if (c == move) return true;
        }
        return false;
    }

    int DoubleWinChecker(char[] moveArray, int n)
    {
        for (int i = 0; i < 9; i++)
        {
            if (moveArray[i] == ' ')
            {
                moveArray[i] = characterByComputer;
                int temp = 0;

                if (n == 1)
                {
                    temp = PairCheck(moveArray, 1);
                    if (temp == -1)
                    {
                        moveArray[i] = ' ';
                        continue;
                    }
                    moveArray[temp] = characterByPlayer;
                    preferredCharacter = characterByPlayer;
                }
                else if (n == 0)
                {
                    preferredCharacter = characterByComputer;
                }

                int result = PairCheck(moveArray, 3);
                if (n == 1) moveArray[temp] = ' ';
                if (result == 690)
                {
                    result = PairCheck(moveArray, 2);
                    moveArray[i] = ' ';
                    if (result != 69) return i;
                }
                else if (n == 1)
                {
                    moveArray[i] = ' ';
                    return i;
                }
                moveArray[i] = ' ';
            }
        }
        return -1;
    }

    int WinGenerator(int[] movesAvailable, char[] moveArray)
    {        
        foreach (int availableMove in movesAvailable)
        {
            if (moveArray[availableMove] == ' ')
            {
                moveArray[availableMove] = characterByComputer;
                int result = PairCheck(moveArray, 0);
                if (result != 690)
                {
                    moveArray[result] = characterByPlayer;
                    int temp = PairCheck(moveArray, 2);
                    moveArray[result] = ' ';
                    moveArray[availableMove] = ' ';
                    if (temp == 690) return availableMove;
                }
                moveArray[availableMove] = ' ';
            }
        }
        return -1;
    }

    int PairCheck(char[] play, int n)
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
            {
                if (previousCharacter == characterByComputer && n == 0)
                    return sum1;
                else if (previousCharacter == characterByPlayer && n == 2)
                    return 69;
                else if (n % 2 == 1)
                    encode += previousCharacter + sum1.ToString();
            }
        }
        if (n % 2 == 0) return 690;

        int l = encode.Length;
        if (n == 3)
        {
            int flag = 0;
            for (int i = 0; i < l; i += 2)
            {
                if (encode[i] == preferredCharacter) flag++;
            }
            if (flag == 2) return 690;
        }
        else if (l > 1 && n == 1)
        {
            int last = 0;
            for (int i = 0; i < l; i += 2)
            {
                if (encode[i] == characterByComputer) return int.Parse(encode[i + 1].ToString());
                else last = i;
            }
            return int.Parse(encode[last + 1].ToString());
        }
        return -1;
    }

    int Controller(char[] play)
    {
        int z = PairCheck(play, 1);
        if (z != -1) return z;
        z = DoubleWinChecker(play, 1);
        if (z != -1) return z;
        return Random(play);
    }

    int FirstMove()
    {
        moveCount++;
        if (CornerCheck(playersFirstMove)) return 4;
        else
        {
            System.Random random = new System.Random();
            firstMove = random.Next(4);
            firstMove = corner[firstMove];
            return firstMove;
        }
    }

    public int Random(char[] play)
    {
        int b = 0;
        int[] rand = new int[9];
        for (int z = 0; z < 9; z++) rand[z] = -1;

        for (int i = 0; i < 9; i++)
        {
            if (play[i] == ' ')
            {
                rand[b] = i;
                b++;
            }
        }

        System.Random random = new System.Random();
        int randomValue = random.Next(b);
        moveCount++;
        return rand[randomValue];
    }

    public int Input(char[] play, int choice, int lastMove)
    {
        if (choice == 2) return Second(play, choice, lastMove);
        else return First(play, choice);
    }

    int First(char[] play, int choice)
    {
        if (moveCount == 0) return FirstMove();
        else if (moveCount > 1 && n == 2) return Controller(play);
        else if (play[4] == ' ' || n == 1)
        {
            n = 1;
            moveCount++;
            return FWin1(play);
        }
        else if (play[4] != ' ')
        {
            Debug.Log(firstMove);
            n = 2;
            moveCount++;
            return 8 - firstMove;
        }
        return 0;
    }

    int FWin1(char[] play)
    {
        if (moveCount == 2) return WinGenerator(corner, play);
        else if (moveCount == 3)
        {
            int move = PairCheck(play, 1);
            n = 2;
            if (move != -1) return move;
            return DoubleWinChecker(play, 0);
        }
        return 0;
    }

    int Second(char[] play, int n, int lastMove)
    {
        if (moveCount == 0)
        {
            playersFirstMove = lastMove;
            return FirstMove();
        }
        moveCount++;
        if (CornerCheck(playersFirstMove) && moveCount == 1)
            return DoubleWinChecker(play, 1);
        return Controller(play);
    }
}
