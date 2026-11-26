// See https://aka.ms/new-console-template for more information

using System.Globalization;

static void AfficherMatrice(int[,] matrice)
{
    int n = matrice.GetLength(0);
    Console.WriteLine();
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            Console.WriteLine(matrice[i,j] + "\t");
        }
    }
    Console.WriteLine();
}
static int[,] CopierMatrice(int[,] matrice)
{
    int n = matrice.GetLength(0);
    int[,] copie = new int[n,n];

    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            copie[i,j] = matrice[i,j];
        }
    }
    return copie;
}

static void TrouverBasesInitiales(int[,] matrice, int[] b)
{
    int n = matrice.GetLength(0);
    for (int i = 0; i < n; i++)
    {
        int min = matrice[i,0];
        int colMin = 0;
        for (int j = 1; j < n; j++)
        {
            if(matrice[i,j] < min){
                min = matrice[i,j];
                colMin = j;
            }
        }
        b[i] = colMin;
    }
}

static void AfficherBases(int[] b)
{
    for (int i = 0; i < b.Length; i++)
    {
        System.Console.WriteLine(b[i] + "\t");
    }
    System.Console.WriteLine();
}

static void CompterBases(int[] b, int[] nb)
{
    int n = b.Length;
    for (int j = 0; j < n; j++)
    {
        nb[j] = 0;
    }
    for (int i = 0; i < n; i++)
    {
        nb[b[i]]++;
    }
}


static int TrouverColonneAvecPlusieursBases(int[] nb)
{
    int n = nb.Length;
    for (int i = 0; i < n; i++)
    {
        if(nb[i] > 1) return i;
    }
    return -1;
}

static void Initialisation(int n, bool[] A, bool[] APrime)
{
    for (int i = 0; i < n; i++)
    {
        A[i] =false;
        APrime[i] = true;
    }
}

