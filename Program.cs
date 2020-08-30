using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

class MainClass
{

    public static void Main(string[] args)
    {
        Menu();
    }

    public static void Menu()
    {
        Console.Clear();
        int opcaoMenu = 0;

        Console.WriteLine("O que deseja fazer?");
        Console.WriteLine("(1) Cadastrar novo produto");
        Console.WriteLine("(2) Ver produtos cadastrados");
        Console.WriteLine("(3) Ver produtos no carinho");
        Console.WriteLine("(4) Esvaziar carinho");
        Console.WriteLine("(5) Finalizar compra");
        Console.WriteLine("(6) Exibir carinhos");
        Console.WriteLine("(7) Cadastrar carinho");
        opcaoMenu = Convert.ToInt32(Console.ReadLine());

        switch (opcaoMenu)
        {
            case 1:
                string interacao;

                Console.Clear();
                try
                {
                    Console.WriteLine("<<<<<<<<< Cadastro de produto >>>>>>>\n");

                    do
                    {
                        Produto produto = new Produto();

                        Console.WriteLine("Nome:");
                        produto.Nome = Console.ReadLine();

                        Console.WriteLine("\nQuantidade:");
                        produto.Quantidade = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("\nPreço Unitário:");
                        produto.PrecoUnitario = Convert.ToDouble(Console.ReadLine());

                        CadastrarProduto(produto);

                        Console.WriteLine("\nDeseja cadastrar outro produto? (s/n)");
                        interacao = Console.ReadLine();
                    }
                    while (interacao.ToLower() == "s");

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Deu ruim: {0}", ex);
                }

                Console.Clear();
                Menu();

                break;
            case 2:
                try
                {
                    VerProduto();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Deu ruim: {0}", ex);
                }
                break;
            case 3:
                try
                {
                    Console.WriteLine("<<<<<<<<< Carinho >>>>>>>>\n");
                    Console.WriteLine("Digite o Id do carinho");
                    VerCarinho(Convert.ToInt32(Console.ReadLine()));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Deu ruim: {0}", ex);
                }
                break;
            case 4:
                try
                {
                    Console.WriteLine("Digite o id do carinho");
                    EsvaziarCarinho(Convert.ToInt32(Console.ReadLine()));

                    Console.WriteLine("\nItens excluídos com sucesso");
                    Thread.Sleep(1000);

                    Menu();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Deu ruim: {0}", ex);
                }
                break;
            case 5:
                try
                {
                    Console.WriteLine("Deseja finalizar a compra? (s/n)");
                    if (Console.ReadLine().ToLower() == "s")
                    {
                        Console.WriteLine("Compra finalizada!");
                    }
                    else
                    {
                        Console.Clear();
                        Menu();
                    }

                }

                catch (Exception ex)
                {
                    Console.WriteLine("Deu ruim: {0}", ex);
                }
                break;

            case 6:
                try
                {
                    Console.WriteLine("<<<<<<<<< Carinhos Cadastrados >>>>>>>\n");

                    ResumoCarinho();
                    Console.WriteLine("Deseja unir mais carinhos? (s/n)");
                    if (Console.ReadLine().ToLower() == "s")
                    {
                        Console.WriteLine("Digite o Id do primeiro carinho");
                        int car1 = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Digite o Id do segundo carinho");
                        int car2 = Convert.ToInt32(Console.ReadLine());

                        somarCarinho(car1, car2);

                        Console.WriteLine("Soma efetuada com sucesso!");
                        Thread.Sleep(1000);
                        Menu();
                    }
                    else
                    {
                        Console.Clear();
                        Menu();
                    }

                }

                catch (Exception ex)
                {
                    Console.WriteLine("Deu ruim: {0}", ex);
                }
                break;

            case 7:
                try
                {
                    CadastrarCarinho();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Deu ruim: {0}", ex);
                }
                break;

            default:
                Console.Clear();
                Console.WriteLine("Digite uma opção válida!");
                Menu();
                break;
        }
    }

    #region itensMenu

    public static void CadastrarCarinho()
    {
        using (Contexto con = new Contexto())
        {
            CarinhoCompra carinho = new CarinhoCompra();

            con.CarinhoCompra.Add(carinho);
            con.SaveChanges();
            Console.WriteLine("Carinho de compra cadastrado com sucesso Id: {0}", carinho.IdCarinhoCompra);
        }

        Console.WriteLine("\nDeseja voltar ao menu anterior? (s/n)");
        if (Console.ReadLine().ToLower() == "s")
        {
            Menu();
        }
    }

    public static void somarCarinho(int idCarinho1, int idCarinho2)
    {
        using (Contexto con = new Contexto())
        {
            CarinhoCompra carinho1 = con.CarinhoCompra.Where(x => x.IdCarinhoCompra == idCarinho1).First();
            CarinhoCompra carinho2 = con.CarinhoCompra.Where(x => x.IdCarinhoCompra == idCarinho2).First();

            CarinhoCompra carinho3 = carinho1 + carinho2;
        }
    }

    public static void ResumoCarinho()
    {
        using (Contexto con = new Contexto())
        {
            var result = con.CarinhoCompra.ToList();

            foreach (var carinho in result)
            {
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("Id Carinho: {0}", carinho.IdCarinhoCompra);
                Console.WriteLine("Total: R$ {0}", carinho.ValorTotal);
            }
        }
    }

    public static void VerProduto()
    {
        List<int> listIdAdicionarCarrinho = new List<int>();

        using (Contexto con = new Contexto())
        {
            List<Produto> listProdutos = con.Produto.ToList();
            int interacao = 0;

            if (listProdutos.Any())
            {
                foreach (Produto produto in listProdutos)
                {
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("Id: {0}", produto.IdProduto);
                    Console.WriteLine("Nome: {0}", produto.Nome);
                    Console.WriteLine("Preço Unitário: {0}", produto.PrecoUnitario);
                    Console.WriteLine("Quantidade: {0}", produto.Quantidade);
                    Console.WriteLine("Código: {0}", produto.Codigo);
                }

                Console.WriteLine("\nDeseja adicionar algum produto no carinho? (s/n)");
            }
            else
            {
                Console.WriteLine("Sem produtos");
                Menu();
            }

            if (Console.ReadLine().ToLower() == "s")
            {
                do
                {
                    Console.WriteLine("Digite o id do produto (Apenas um por vez)");
                    listIdAdicionarCarrinho.Add(Convert.ToInt32(Console.ReadLine()));

                    Console.WriteLine("Repetir ação? (s/n)");
                    if (Console.ReadLine().ToLower() != "s")
                    {
                        interacao = 1;
                        Console.WriteLine("Digite o id do carinho");
                        AdicionarCarinho(listIdAdicionarCarrinho, Convert.ToInt32(Console.ReadLine()));

                        Menu();
                    }

                }
                while (interacao == 0);
            }
            else
            {
                Console.WriteLine("\nDeseja voltar ao menu anterior? (s/n)");
                if (Console.ReadLine().ToLower() == "s")
                {
                    Menu();
                }
            }
        }

    }

    /// <summary>
    /// Esvazia carinho, neste caso o escopo e de apenas um
    /// </summary>
    public static void EsvaziarCarinho(int idCarinho)
    {
        using (Contexto con = new Contexto())
        {
            var result = con.ItemCompra.Where(x => x.IdCarinhoCompra == idCarinho).ToList();

            con.ItemCompra.RemoveRange(result);

            CarinhoCompra carinho = con.CarinhoCompra.Where(x => x.IdCarinhoCompra == idCarinho).First();
            carinho.ValorTotal = "";

            con.SaveChanges();
        }

    }

    public static void AdicionarCarinho(List<int> ids, int idCarinho)
    {
        using (Contexto con = new Contexto())
        {
            foreach (int id in ids)
            {
                con.ItemCompra.Add(new ItemCompra
                {
                    IdProduto = id,
                    IdCarinhoCompra = idCarinho,
                });

                con.SaveChanges();
            }
        }
    }

    public static void CadastrarProduto(Produto produto)
    {
        Random rnd = new Random();
        int id;

        using (Contexto con = new Contexto())
        {
            con.Produto.Add(produto);
            con.SaveChanges();

            id = produto.IdProduto;

            produto = con.Produto.Where(x => x.IdProduto == id).First();
            produto.Codigo = rnd.Next() + produto.IdProduto;

            con.SaveChanges();
        }

    }

    /// <summary>
    /// Só existe um carinho no sistema seguindo a especificações do problema
    /// </summary>
    public static void VerCarinho(int idCarinho)
    {
        Console.Clear();

        double totalCarinho = 0;

        using (Contexto con = new Contexto())
        {
            CarinhoCompra result = con.CarinhoCompra
                                        .Where(x => x.IdCarinhoCompra == idCarinho)
                                        .Include(x => x.ItemCompra)
                                        .ThenInclude(x => x.IdProdutoNavigation)
                                        .First();

            if (result.ItemCompra.Any())
            {


                foreach (ItemCompra item in result.ItemCompra)
                {
                    totalCarinho += item.IdProdutoNavigation.PrecoUnitario.Value;

                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("Id: {0}", item.IdProdutoNavigation.IdProduto);
                    Console.WriteLine("Nome: {0}", item.IdProdutoNavigation.Nome);
                    Console.WriteLine("Preço Unitário: {0}", item.IdProdutoNavigation.PrecoUnitario);
                    Console.WriteLine("Código: {0}", item.IdProdutoNavigation.Codigo);
                }
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("Total: {0}", totalCarinho);

                CarinhoCompra carinho = con.CarinhoCompra.Where(x => x.IdCarinhoCompra == idCarinho).First();
                carinho.ValorTotal = totalCarinho.ToString();
                con.SaveChanges();
            }
            else
            {
                Console.WriteLine("\nCarrinho vazio");
                Thread.Sleep(1000);

                Menu();
            }
        }
        Console.WriteLine("\nDeseja voltar ao menu anterior? (s/n)");
        if (Console.ReadLine().ToLower() == "s")
        {
            Menu();
        }
    }

    #endregion

}