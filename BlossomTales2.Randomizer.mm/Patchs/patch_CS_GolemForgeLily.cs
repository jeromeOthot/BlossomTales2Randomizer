// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_CS_GolemForgeLily : CS_GolemForgeLily
    {
        private Puppet golem;
        private Puppet masterSword;

        public void talkGolem()
	{
		Game1.player.Direction = 1;
		golem.play("talk");
		golem.bounce();
		if (Game1.Globals.GolemForge_State == 0)
		{
			Game1.Globals.GolemForge_State = 1;
			Game1.Dialoger.AddLine("Golem: Oh... hello there!");
			Game1.Dialoger.AddLine("Golem: You don't look much like a minotaur.");
			Game1.Dialoger.AddLine("Golem: Oh, you're going after the <A>Minotaur <A>King to rescue your brother?");
			Game1.Dialoger.AddLine("Golem: I could help you on your quest... for a price.");
			Game1.Dialoger.AddLine("Golem: I'll forge a powerful ability into your sword for <D>49 <D>gems.");
			Game1.Dialoger.AddLine("Golem: But I also need 1 gem for forging. So, <D>50 <D>Gems total. Bring me <D>50 <D>Gems.");
			if (Game1.player.Count_Gems > 49)
			{
				//if (Game1.player.SwordLevel == 3)
				{
					Game1.Dialoger.AddLine("Golem: I see you already have <D>50 <D>Gems on you.");
					Game1.Dialoger.AddLine("Golem: Place your sword before me to begin.", "doSwordUpgrade", new string[2] { "Upgrade sword", "Not right now" });
				}
				/*else
				{
					Game1.Dialoger.AddLine("Golem: I see you already have <D>50 <D>Gems on you, but that sword you have simply won't do.");
					Game1.Dialoger.AddLine("Golem: I can't upgrade such a rusty piece of junk.");
					Game1.Dialoger.AddLine("Golem: Go and seek a great sword and bring it before me.");
					Game1.Dialoger.AddLine("Golem: Only then will I grant you my great forging abilities.", goPlayer);
				}*/
			}
			else// if (Game1.player.SwordLevel == 3)
			{
				Game1.Dialoger.AddLine("Golem: Come back when you have collected <D>50 <D>Gems. Golem hungry.");
				Game1.Dialoger.AddLine("Golem: Only then will I grant you my great forging abilities.", goPlayer);
			}
			/*else
			{
				Game1.Dialoger.AddLine("Golem: Even if you had the <D>50 <D>Gems, that sword you're carrying simply won't do.");
				Game1.Dialoger.AddLine("Golem: I can't upgrade such a rusty piece of junk.");
				Game1.Dialoger.AddLine("Golem: Go and seek a great sword and bring it before me.");
				Game1.Dialoger.AddLine("Golem: Only then will I grant you my great forging abilities.", goPlayer);
			}*/
		}
		else if (Game1.Globals.GolemForge_State == 1)
		{
			if (Game1.player.Count_Gems > 49)
			{
				//if (Game1.player.SwordLevel == 3)
				{
					Game1.Dialoger.AddLine("Golem: I see you have collected <D>50 <D>Gems.");
					Game1.Dialoger.AddLine("Golem: Place your sword before me to begin.", "doSwordUpgrade", new string[2] { "Upgrade sword", "Not right now" });
				}
				/*else
				{
					Game1.Dialoger.AddLine("Golem: I see you have collected <D>50 <D>Gems, but that sword you have simply won't do.");
					Game1.Dialoger.AddLine("Golem: I can't upgrade such a rusty piece of junk.");
					Game1.Dialoger.AddLine("Golem: Go and seek a great sword and bring it before me.");
					Game1.Dialoger.AddLine("Golem: Only then will I grant you my great forging abilities.", goPlayer);
				}*/
			}
			else //if (Game1.player.SwordLevel == 3)
			{
				Game1.Dialoger.AddLine("Golem: Come back when you have collected <D>50 <D>Gems.");
				Game1.Dialoger.AddLine("Golem: Only then will I grant you my great forging abilities.", goPlayer);
			}
			/*else
			{
				Game1.Dialoger.AddLine("Golem: Even if you had the <D>50 <D>Gems, that sword you're carrying simply won't do.");
				Game1.Dialoger.AddLine("Golem: I can't upgrade such a rusty piece of junk.");
				Game1.Dialoger.AddLine("Golem: Go and seek a great sword and bring it before me, plus the <D>50 <D>Gems.");
				Game1.Dialoger.AddLine("Golem: Only then will I grant you my great forging abilities.", goPlayer);
			}*/
		}
		else if (Game1.Globals.GolemForge_State == 3)
		{
			Game1.Dialoger.AddLine("Golem: Hope you are enjoying the new sword.", goPlayer);
		}
	}

        public void getSword()
        {
            Game1.Globals.GolemForge_State = 3;
            Mod_GiveItem();
            Game1.player.Velocity.Y = 8f;
            Game1.playSoundCue("hop_11");
            Game1.player.Health = Game1.player.MaxHealth;
            Game1.Achievementer.CheckAchievment(6);
            tweener.Timer(0.2f).OnComplete(delegate
            {
                masterSword.play("hide");
                tweener.Timer(2f).OnComplete(delegate
                {
                    Game1.Dialoger.AddLine("Golem: Take this <B>Sword. You now posses the most powerful weapon in all the lands.");
                    Game1.Dialoger.AddLine("Golem: When you're at full health, this sword will shoot a powerful laser blast.", goPlayer);
                });
            });
        }

        private void Mod_GiveItem()
        {
            RandomizerSingleton.Instance.GiveItemAtLocation(golem.name, Vector3.Zero);
        }
    }
}
