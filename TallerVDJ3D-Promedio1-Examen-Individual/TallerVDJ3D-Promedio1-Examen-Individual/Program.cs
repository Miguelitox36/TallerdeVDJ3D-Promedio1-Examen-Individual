using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerVDJ3D_Promedio1_Examen_Individual
{
    internal class Program
    {
        static Player player;
        static List<Enemy> enemies = new List<Enemy>();

        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido Dungeon Master!");

            CreatePlayer();
            CreateEnemies();
            CreateItems();
            CreatePowers();

            Console.WriteLine("\n¡Comienza la batalla!");

            while (player.IsAlive() && enemies.Any(e => e.IsAlive()))
            {
                PlayerTurn();
                EnemyTurn();
            }

            if (player.IsAlive())
                Console.WriteLine("\n¡Ganaste la batalla!");
            else
                Console.WriteLine("\nPerdiste... el jugador ha muerto.");
        }

        static void CreatePlayer()
        {
            Console.Write("Ingresa el nombre del jugador: ");
            string name = Console.ReadLine();
            player = new Player(name, 100, 10, 50);
            Console.WriteLine($"Jugador {name} creado con 100 de vida, 10 de daño, 50 de maná.");
        }

        static void CreateEnemies()
        {
            Console.Write("¿Cuántos enemigos quieres crear?: ");
            int count = int.Parse(Console.ReadLine());
            for (int i = 0; i < count; i++)
            {
                Console.Write($"Nombre del enemigo {i + 1}: ");
                string name = Console.ReadLine();
                Console.Write("¿Tipo de enemigo? (melee/rango): ");
                string type = Console.ReadLine().ToLower();

                int health = (type == "melee") ? 50 : 30;
                int damage = (type == "melee") ? 15 : 10;

                enemies.Add(new Enemy(name, health, damage));
                Console.WriteLine($"Enemigo {name} creado ({type}), vida: {health}, daño: {damage}.");
            }
        }

        static void CreateItems()
        {
            Console.Write("¿Cuántos items deseas crear para el jugador?: ");
            int itemCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < itemCount; i++)
            {
                Console.Write("Nombre del item: ");
                string name = Console.ReadLine();
                Console.Write("¿Tipo de item? (vida/mana): ");
                string typeInput = Console.ReadLine().ToLower();
                ItemType type = (typeInput == "vida") ? ItemType.Health : ItemType.Mana;
                Console.Write("Cantidad que recupera: ");
                int amount = int.Parse(Console.ReadLine());

                player.Items.Add(new Item(name, type, amount));
            }
        }

        static void CreatePowers()
        {
            Console.Write("¿Cuántos poderes deseas crear para el jugador?: ");
            int powerCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < powerCount; i++)
            {
                Console.Write("Nombre del poder: ");
                string name = Console.ReadLine();
                Console.Write("Daño del poder: ");
                int damage = int.Parse(Console.ReadLine());
                Console.Write("Costo de maná: ");
                int manaCost = int.Parse(Console.ReadLine());

                player.Powers.Add(new Power(name, damage, manaCost));
            }
        }

        static void PlayerTurn()
        {
            Console.WriteLine($"\nTurno de {player.Name}");
            Console.WriteLine($"Vida: {player.Health} | Maná: {player.Mana}");

            Console.WriteLine("¿Qué quieres hacer?");
            Console.WriteLine("1. Usar item");
            Console.WriteLine("2. Atacar enemigo");

            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                if (player.Items.Count == 0)
                {
                    Console.WriteLine("No tienes items.");
                }
                else
                {
                    for (int i = 0; i < player.Items.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {player.Items[i].Name} ({player.Items[i].Type})");
                    }
                    Console.Write("Elige item: ");
                    int itemIndex = int.Parse(Console.ReadLine()) - 1;
                    player.UseItem(player.Items[itemIndex]);
                }
                PlayerTurn(); // volver al turno para forzar ataque
            }
            else if (choice == 2)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {enemies[i].Name} - Vida: {enemies[i].Health}");
                }
                Console.Write("Elige enemigo para atacar: ");
                int enemyIndex = int.Parse(Console.ReadLine()) - 1;
                if (!enemies[enemyIndex].IsAlive())
                {
                    Console.WriteLine("Este enemigo ya está muerto.");
                    PlayerTurn();
                    return;
                }

                Console.WriteLine("¿Cómo quieres atacar?");
                Console.WriteLine("1. Ataque simple");
                Console.WriteLine("2. Usar poder");

                int attackChoice = int.Parse(Console.ReadLine());

                if (attackChoice == 1)
                {
                    enemies[enemyIndex].TakeDamage(player.Damage);
                    Console.WriteLine($"Atacaste causando {player.Damage} de daño.");
                }
                else if (attackChoice == 2)
                {
                    if (player.Powers.Count == 0)
                    {
                        Console.WriteLine("No tienes poderes.");
                        PlayerTurn();
                        return;
                    }

                    for (int i = 0; i < player.Powers.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {player.Powers[i].Name} (Daño: {player.Powers[i].Damage} | Costo: {player.Powers[i].ManaCost})");
                    }
                    Console.Write("Elige poder: ");
                    int powerIndex = int.Parse(Console.ReadLine()) - 1;

                    player.UsePower(player.Powers[powerIndex], enemies[enemyIndex]);
                }

                if (!enemies[enemyIndex].IsAlive() && enemies[enemyIndex].Items.Count > 0)
                {
                    Console.WriteLine($"El enemigo {enemies[enemyIndex].Name} murió. ¡Recuperaste sus items!");
                    player.Items.AddRange(enemies[enemyIndex].Items);
                    enemies[enemyIndex].Items.Clear();
                }
            }
        }

        static void EnemyTurn()
        {
            Enemy attacker = enemies.FirstOrDefault(e => e.IsAlive());
            if (attacker != null)
            {
                Console.WriteLine($"\nTurno de {attacker.Name}");
                player.TakeDamage(attacker.Damage);
                Console.WriteLine($"{attacker.Name} te causó {attacker.Damage} de daño.");
            }
        }
    }
}
