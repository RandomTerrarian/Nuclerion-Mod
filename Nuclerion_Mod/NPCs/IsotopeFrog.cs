using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Nuclerion_Mod.NPCs
{
	public class IsotopeFrog : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Isotope Frog");
			Main.npcFrameCount[npc.type] = 4;
		}
        public override float SpawnChance(NPCSpawnInfo spawnInfo) 
        {
	    return SpawnCondition.OverworldDaySlime.Chance * 20.1f;
        }
		public override void SetDefaults()
		{
			npc.width = 50;
			npc.height = 40;
			npc.damage = 20;
			npc.defense = 0;
			npc.lifeMax = 150;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = 300f;
			npc.knockBackResist = 0.5f;
        }
        public override void FindFrame(int frameHeight) // Animation.
        {
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }
        public override void NPCLoot()
		{
			// Put your drops here.
		}
        private float JumpTime
        {
            get
            {
                return npc.ai[0];
            }
            set
            {
                npc.ai[0] = value;
            }
        }
        private float Animate
        {
            get
            {
                return npc.ai[1];
            }
            set
            {
                npc.ai[1] = value;
            }
        }
        public override void AI()
        {
            npc.frameCounter = 0;
            Player player = Main.player[npc.target];
            if (Animate >= 30)
                npc.frameCounter = 1;
            if (Animate >= 40)
                npc.frameCounter = 2;
            if (Animate >= 50)
            {
                npc.frameCounter = 1;
                Animate = 0;
            }
            if (npc.velocity.X != 0 && npc.velocity.Y != 0)
            {
                npc.frameCounter = 3;
                Animate = 0;
            }
            if (JumpTime == 180)
            {
                Vector2 PlayerPos = player.Center;
                if (PlayerPos.X > npc.Center.X)
                {
                    npc.velocity = new Vector2(10, -8);
                }
                if (PlayerPos.X < npc.Center.X)
                {
                    npc.velocity = new Vector2(-10, -8);
                }
                JumpTime = 0;
                Animate = 0;
            }
            JumpTime++;
            Animate++;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life < 1)
            {
                for (int a = 0; a < 24; a++)
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, 75, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 120, Color.Green, 1.5f);
                    Main.dust[dust].velocity *= 3f;
                    Main.dust[dust].noGravity = true;
                }
            }
            else
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 75, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 120, Color.Green, 1.5f);
            }
        }
    }
}
