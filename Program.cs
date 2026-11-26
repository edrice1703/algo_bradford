    static int[] Bases(int[,] M)
    {
        int n = M.GetLength(0);
        int[] b = new int[n];

        for (int i = 0; i < n; i++)
        {
            b[i] = 0;

            for (int j = 1; j < n; j++)
            {
                if (M[i, j] < M[i, b[i]])
                    b[i] = j;
            }
        }

        return b;
    }


    // 2. Compter les bases par colonne
    static int[] CompterBases(int[] b)
    {
        int n = b.Length;
        int[] nb = new int[n];

        for (int i = 0; i < n; i++)
            nb[b[i]]++;

        return nb;
    }


    // 3. Trouver une colonne de A' contenant plusieurs bases
    static int ChercherC(int[] nb, bool[] A)
    {
        for (int j = 0; j < nb.Length; j++)
        {
            if (!A[j] && nb[j] > 1)
                return j;
        }

        return -1;
    }

    // 4. Calculer Delta = min(a'i - bi)
    static int Delta(int[,] M, int[] b, bool[] A, bool[] lignes, out int ligne, out int colonne)
    {
        int n = M.GetLength(0);

        int d = Int32.MaxValue;
        ligne = -1;
        colonne = -1;

        for (int i = 0; i < n; i++)
        {
            if (!lignes[i])
                continue;

            int bi = M[i, b[i]];

            for (int j = 0; j < n; j++)
            {
                if (!A[j])
                {
                    int x = M[i, j] - bi;

                    if (x < d)
                    {
                        d = x;
                        ligne = i;
                        colonne = j;
                    }
                }
            }
        }

        return d;
    }


    // 5. Algorithme Mack-Bradford
    static int[] MackBradford(int[,] originale)
    {
        int n = originale.GetLength(0);

        // Matrice de travail
        int[,] M = new int[n, n];

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                M[i, j] = originale[i, j];

        // Bases initiales
        int[] b = Bases(M);

        while (true)
        {
            int[] nb = CompterBases(b);

            // Une base par colonne => terminé
            bool fini = true;

            for (int j = 0; j < n; j++)
            {
                if (nb[j] != 1) fini = false;
            }

            if (fini) return b;

            // -------------------------------
            // A = vide ; A' = toutes les colonnes
            // -------------------------------

            bool[] A = new bool[n];

            for (int j = 0; j < n; j++)
                A[j] = false;


            // Etape 1 : chercher C
            int C = ChercherC(nb, A);

            if (C == -1)
                return b;

            A[C] = true;


            // Lignes dont la base est dans A
            bool[] lignes = new bool[n];

            for (int i = 0; i < n; i++)
            {
                if (A[b[i]])
                    lignes[i] = true;
            }

            // --------------------------------
            // Construction progressive de A
            // --------------------------------

            while (true)
            {
                // Etape 2 : calcul de Delta
                int ligne;
                int colonne;

                int d = Delta(
                    M, b, A, lignes,
                    out ligne,
                    out colonne
                );

                // Etape 3 :
                // augmenter les colonnes de A de Delta

                for (int j = 0; j < n; j++)
                {
                    if (A[j])
                    {
                        for (int i = 0; i < n; i++)
                            M[i, j] += d;
                    }
                }
                // --------------------------------
                // Chercher le nouvel élément minimum
                // dans A'
                // --------------------------------

                int C2 = colonne;
                // C2 est la colonne contenant a'i
                // Si elle contient déjà une base :
                // A' -> A
                // puis on recommence l'étape 2.

                if (nb[C2] > 0)
                {
                    A[C2] = true;

                    for (int i = 0; i < n; i++)
                    {
                        if (b[i] == C2)
                            lignes[i] = true;
                    }

                    continue;
                }

                b[ligne] = C2;

                break;
            }
        }
    }


    // 6. Afficher matrice + affectation
    static void Afficher(int[,] M, int[] b)
    {
        int n = M.GetLength(0);
        int cout = 0;

        Console.WriteLine("\nAFFECTATION FINALE");

        for (int i = 0; i < n; i++)
        {
            cout += M[i, b[i]];
        }

        Console.WriteLine("Cout total = " + cout);
    }
    // 7. Programme principal
    static void Main()
    {
        int[,] M =
        {
            { 8, 2, 6, 7, 5 },
            { 3, 4, 9, 3, 8 },
            { 7, 6, 2, 8, 4 },
            { 4, 9, 5, 3, 6 },
            { 6, 5, 4, 7, 2 }
        };

        int[] solution = MackBradford(M);

        Afficher(M, solution);
    }