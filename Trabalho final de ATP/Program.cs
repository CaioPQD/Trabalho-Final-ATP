using System;
using System.IO;
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            int opcao = 0;
            int x = 4;

            string[] produtos = new string[2];
            string[] nome = new string[x];
            int[] qtd = new int[x];
            string[,] matrizVenda = new string[1, 4];
            matrizVenda[0, 0] = "Dia";
            matrizVenda[0, 1] = "Nº do Produto";
            matrizVenda[0, 2] = "Nome";
            matrizVenda[0, 3] = "Quantidade Vendida";

            while (opcao != 6)
            {
                Console.WriteLine("----------Menu Principal----------");
                Console.WriteLine("1  -< Importar arquivo de produtos >-");
                Console.WriteLine("2  -< Registrar venda >-");
                Console.WriteLine("3  -< Relatório de vendas >-");
                Console.WriteLine("4  -< Relatório de estoque >-");
                Console.WriteLine("5  -< Criar arquivo de vendas >-");
                Console.WriteLine("6  -< Sair >-");
                Console.Write("-< Digite a opção desejada: >-");
                opcao = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (opcao)
                {
                    case 1:
                        Console.WriteLine("=========== IMPORTAR ARQUIVO DE PRODUTOS ===========");
                        Console.WriteLine();

                        using (StreamReader sr = new StreamReader("Produtos.txt"))
                        {
                            // Lê o cabeçalho
                            string linha = sr.ReadLine();

                            while (linha != null)
                            {
                                linha = sr.ReadLine();
                                if (linha != null)
                                {
                                    produtos = linha.Split(";");
                                    nome[i] = produtos[0];
                                    qtd[i] = int.Parse(produtos[1]);
                                    i++;
                                }
                            }
                        }

                        Console.WriteLine("<<<< O arquivo foi importado com sucesso ! >>>>");
                        Console.WriteLine();
                        break;

                    case 2:
                        Console.WriteLine("=========== REGISTRAR VENDA ===========");
                        Console.WriteLine();

                        int numProd, dia, qtdVend;
                        Console.Write("-> Digite o número do produto (1 a 4): ");
                        numProd = int.Parse(Console.ReadLine());

                        if (numProd > 0 && numProd <= 4)
                        {
                            Console.Write("-> Digite o dia do mês em que foi realizada a venda: ");
                            dia = int.Parse(Console.ReadLine());

                            Console.Write("-> Digite a quantidade vendida do produto: ");
                            qtdVend = int.Parse(Console.ReadLine());

                            if (qtdVend <= qtd[numProd - 1])
                            {
                                var aux = RegistraVenda(numProd, dia, qtdVend, nome, qtd, matrizVenda);
                                matrizVenda = new string[aux.GetLength(0), 4];
                                matrizVenda = aux;
                                Console.WriteLine("<<<< Venda registrada e realizada com exito ! >>>>");
                            }
                            else
                            {
                                Console.WriteLine(">>>> Não foi possivel realizar a venda, não há a quantidade informada em estoque no momento. <<<<");
                            }
                        }
                        else
                        {
                            Console.WriteLine(">>>> Número do produto inválido ! <<<<");
                        }

                        Console.WriteLine();
                        break;

                case 3:
                        Console.WriteLine("==================== RELATÓRIO DE VENDAS ====================");
                        ExibirRelatorioVendas(matrizVenda);
                        Console.WriteLine();
                        break;

                    case 4:
                        Console.WriteLine("=========== RELATÓRIO DE ESTOQUE ===========");
                        Console.WriteLine();
                        ExibirRelatorioEstoque(nome, qtd);
                        Console.WriteLine("<<<< Relatorio gerado com exito ! >>>>");
                        Console.WriteLine();
                        break;

                    case 5:
                        Console.WriteLine("=========== CRIAR ARQUIVO DE VENDAS ===========");
                        Console.WriteLine();
                        using (StreamWriter sw = new StreamWriter("RelatorioGeralDeVendas.txt"))
                        {
                            for (int v = 0; v < matrizVenda.GetLength(0); v++)
                            {
                                for (int j = 0; j < matrizVenda.GetLength(1); j++)
                                {
                                    if (j == matrizVenda.GetLength(1) - 1)
                                        sw.Write(matrizVenda[v, j]);
                                    else
                                        sw.Write(matrizVenda[v, j] + ";");
                                }
                                sw.WriteLine();
                            }
                        }
                        Console.WriteLine("<<<< Arquivo gerado com exito ! >>>>");
                        Console.WriteLine();
                        break;

                    case 6:
                        Console.WriteLine("============== ENCERRANDO ==============");
                        break;

                    default:
                        Console.WriteLine(" >>>> Opção inválida. Tente novamente ! <<<<");
                        Console.WriteLine();
                        break;
                }
            }
        }

        static void PreencheMatriz(string[,] matrizPreencher, string[,] matrizComConteudo)
        {
            for (int i = 0; i < matrizComConteudo.GetLength(0); i++)
            {
                for (int j = 0; j < matrizPreencher.GetLength(1); j++)
                {
                    matrizPreencher[i, j] = matrizComConteudo[i, j];
                }
            }
        }

         static string[,] RegistraVenda(int numProd, int dia, int qtdVendida, string[] nome, int[] qtd, string[,] matriz)
        {
            string[,] novaMatriz = new string[matriz.GetLength(0) + 1, matriz.GetLength(1)];
            PreencheMatriz(novaMatriz, matriz);

            novaMatriz[matriz.GetLength(0), 0] = dia.ToString();
            novaMatriz[matriz.GetLength(0), 1] = numProd.ToString();
            novaMatriz[matriz.GetLength(0), 2] = nome[numProd - 1];
            novaMatriz[matriz.GetLength(0), 3] = qtdVendida.ToString();

            qtd[numProd - 1] -= qtdVendida;

            return novaMatriz;
        }

         static void ExibirRelatorioVendas(string[,] matrizVenda)
        {
            for (int v = 0; v < matrizVenda.GetLength(0); v++)
            {
                for (int j = 0; j < matrizVenda.GetLength(1); j++)
                {
                    Console.Write(string.Format("{0,-10}", matrizVenda[v, j]) + "\t");
                }
                Console.WriteLine();
            }
        }

         static void ExibirRelatorioEstoque(string[] nome, int[] qtd)
        {
            Console.WriteLine("Produto\t\tQuantidade");
            for (int e = 0; e < nome.Length; e++)
            {
                Console.WriteLine("{0}\t\t{1}", nome[e], qtd[e]);
            }
        }
    }
