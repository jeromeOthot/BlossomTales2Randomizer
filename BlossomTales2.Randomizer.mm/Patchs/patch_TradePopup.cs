using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_TradePopup : TradePopup
    {
        private ItemData itemToGet;

        //Copying the whole method in case we want to randomize the items to trade and their quantities.
        public void OpenMenu(int idIndex)
	    {
		    IsOpen = true;
		    IDNumber = idIndex;
		    if (IDNumber == 0)
		    {
			    youGive = new EquipableItem.IngredientList[3]
			    {
				    EquipableItem.IngredientList.Apple,
				    EquipableItem.IngredientList.Mushroom,
				    EquipableItem.IngredientList.Clover
			    };
			    youNeed = new int[3] { 8, 5, 4 };
                Mod_SetYouGetItem("traderStan0");
            }
		    else if (IDNumber == 1)
		    {
			    youGive = new EquipableItem.IngredientList[3]
			    {
				    EquipableItem.IngredientList.Chrysanthemum,
				    EquipableItem.IngredientList.Orange,
				    EquipableItem.IngredientList.Willow
			    };
			    youNeed = new int[3] { 8, 11, 7 };
                Mod_SetYouGetItem("traderStan1");
		    }
		    else if (IDNumber == 2)
		    {
			    youGive = new EquipableItem.IngredientList[3]
			    {
				    EquipableItem.IngredientList.Skyblossom,
				    EquipableItem.IngredientList.Clam,
				    EquipableItem.IngredientList.Snailshell
			    };
			    youNeed = new int[3] { 8, 4, 5 };
                Mod_SetYouGetItem("traderStan2");
		    }
		    else if (IDNumber == 3)
		    {
			    youGive = new EquipableItem.IngredientList[3]
			    {
				    EquipableItem.IngredientList.CanyonWisp,
				    EquipableItem.IngredientList.Jojoba,
				    EquipableItem.IngredientList.Sunkiss
			    };
			    youNeed = new int[3] { 6, 9, 5 };
                Mod_SetYouGetItem("traderStan3");
		    }
		    else if (IDNumber == 4)
		    {
			    youGive = new EquipableItem.IngredientList[3]
			    {
				    EquipableItem.IngredientList.DesertPuff,
				    EquipableItem.IngredientList.WaterDrop,
				    EquipableItem.IngredientList.FlameTongue
			    };
			    youNeed = new int[3] { 6, 5, 7 };
                Mod_SetYouGetItem("traderStan4");
		    }
		    else if (IDNumber == 5)
		    {
			    youGive = new EquipableItem.IngredientList[3]
			    {
				    EquipableItem.IngredientList.Aster,
				    EquipableItem.IngredientList.Lily,
				    EquipableItem.IngredientList.RootWeed
			    };
			    youNeed = new int[3] { 8, 9, 8 };
                Mod_SetYouGetItem("traderStan5");
		    }
		    else if (IDNumber == 6)
		    {
			    youGive = new EquipableItem.IngredientList[3]
			    {
				    EquipableItem.IngredientList.RedMushroom,
				    EquipableItem.IngredientList.GreenMushroom,
				    EquipableItem.IngredientList.PurpleMushroom
			    };
			    youNeed = new int[3] { 9, 8, 9 };
                Mod_SetYouGetItem("traderStan6");
		    }
		    else if (IDNumber == 7)
		    {
			    youGive = new EquipableItem.IngredientList[3]
			    {
				    EquipableItem.IngredientList.Poinsettia,
				    EquipableItem.IngredientList.Bellflower,
				    EquipableItem.IngredientList.Daisy
			    };
			    youNeed = new int[3] { 8, 8, 8 };
                Mod_SetYouGetItem("traderStan7");
		    }
		    else if (IDNumber == 20)
		    {
			    youGive = new EquipableItem.IngredientList[3]
			    {
				    EquipableItem.IngredientList.Fish1,
				    EquipableItem.IngredientList.Fish3,
				    EquipableItem.IngredientList.Fish2
			    };
			    youNeed = new int[3] { 4, 3, 4 };
                Mod_SetYouGetItem("traderFish20");
		    }
		    else if (IDNumber == 21)
		    {
			    youGive = new EquipableItem.IngredientList[3]
			    {
				    EquipableItem.IngredientList.Fish1,
				    EquipableItem.IngredientList.Fish3,
				    EquipableItem.IngredientList.Fish5
			    };
			    youNeed = new int[3] { 3, 3, 3 };
                Mod_SetYouGetItem("traderFish21");
		    }
		    else if (IDNumber == 22)
		    {
			    youGive = new EquipableItem.IngredientList[3]
			    {
				    EquipableItem.IngredientList.Fish5,
				    EquipableItem.IngredientList.Fish4,
				    EquipableItem.IngredientList.Fish2
			    };
			    youNeed = new int[3] { 4, 5, 4 };
                Mod_SetYouGetItem("traderFish22");
		    }
		    else if (IDNumber == 23)
		    {
			    youGive = new EquipableItem.IngredientList[3]
			    {
				    EquipableItem.IngredientList.Fish8,
				    EquipableItem.IngredientList.Fish6,
				    EquipableItem.IngredientList.Fish4
			    };
			    youNeed = new int[3] { 2, 2, 2 };
                Mod_SetYouGetItem("traderFish23");
		    }
		    else if (IDNumber == 24)
		    {
			    youGive = new EquipableItem.IngredientList[3]
			    {
				    EquipableItem.IngredientList.Fish5,
				    EquipableItem.IngredientList.Fish6,
				    EquipableItem.IngredientList.Fish7
			    };
			    youNeed = new int[3] { 4, 3, 3 };
                Mod_SetYouGetItem("traderFish24");
		    }
		    else if (IDNumber == 25)
		    {
			    youGive = new EquipableItem.IngredientList[3]
			    {
				    EquipableItem.IngredientList.Fish8,
				    EquipableItem.IngredientList.Fish9,
				    EquipableItem.IngredientList.Fish10
			    };
			    youNeed = new int[3] { 2, 2, 2 };
                Mod_SetYouGetItem("traderFish25");
		    }
		    else if (IDNumber == 50)
		    {
			    youGive = new EquipableItem.IngredientList[3]
			    {
				    EquipableItem.IngredientList.Spikeshell,
				    EquipableItem.IngredientList.Clam,
				    EquipableItem.IngredientList.Snailshell
			    };
			    youNeed = new int[3] { 5, 5, 5 };
			    youGet = EquipableItem.ItemList.Empty;
		    }
		    else if (IDNumber == 51)
		    {
			    youGive = new EquipableItem.IngredientList[3]
			    {
				    EquipableItem.IngredientList.Fish7,
				    EquipableItem.IngredientList.Fish3,
				    EquipableItem.IngredientList.Fish8
			    };
			    youNeed = new int[3] { 5, 5, 5 };
			    youGet = EquipableItem.ItemList.Empty;
		    }
		    else if (IDNumber == 52)
		    {
			    youGive = new EquipableItem.IngredientList[3]
			    {
				    EquipableItem.IngredientList.Fish7,
				    EquipableItem.IngredientList.Fish8,
				    EquipableItem.IngredientList.Fish3
			    };
			    youNeed = new int[3] { 3, 3, 3 };
			    youGet = EquipableItem.ItemList.Empty;
		    }
		    else
		    {
			    youGive = new EquipableItem.IngredientList[3]
			    {
				    EquipableItem.IngredientList.Apple,
				    EquipableItem.IngredientList.Orange,
				    EquipableItem.IngredientList.Jojoba
			    };
			    youNeed = new int[3] { 3, 5, 2 };
			    youGet = EquipableItem.ItemList.Jar_Empty;
		    }
	    }

        private void Mod_SetYouGetItem(string itemId)
        {
            itemToGet = RandomizerSingleton.Instance.GetItemAtLocation(string.Empty, itemId, Vector3.Zero);
            youGet = (EquipableItem.ItemList)itemToGet.Item;
        }
    }
}
