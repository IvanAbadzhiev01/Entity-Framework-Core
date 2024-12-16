using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.AddMinion
{
    public class SqlQyery
    {
        public const string GetTownByName = @"SELECT Id FROM Towns WHERE Name = @townName";

        public const string GetVilainByName = @"SELECT Id FROM Villains WHERE Name = @Name";

        
        
        
        



        public const string AddVilain = @"INSERT INTO Villains (Name, EvilnessFactorId) OUTPUT inserted.Id  VALUES (@villainName, 4)";


        public const string AddTown = @"INSERT INTO Towns (Name), OUTPUT inserted.Id VALUES (@townName)";


        public const string AddMinion = @"INSERT INTO Minions (Name, Age, TownId) OUTPUT inserted.Id VALUES (@name, @age, @townId)";


        public const string insertMv = @"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@minionId, @villainId)";



    }
}
