using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace FestivalTalk
{
    internal sealed class ModEntry : Mod
    {
        private bool SeenEvent = false;
        public override void Entry(IModHelper helper)
        {

            helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
            helper.Events.GameLoop.DayStarted += OnDayStarted;
        }

        private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            foreach (KeyValuePair<string, Friendship> pair in Game1.player.friendshipData.Pairs)
            {
                string name = pair.Key;
                Friendship friendship = pair.Value;
                string points = friendship.Points.ToString();
                this.Monitor.Log($"{points} with this {name}", LogLevel.Info);

            }
        }

        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            // refresh data
            this.SeenEvent = false;
            this.Monitor.Log("a new day!!", LogLevel.Debug);
        }

        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;


            if (SeenEvent == false && Game1.CurrentEvent?.isFestival == true) 
            {
                SeenEvent = true;

                string[] talkedToNPCS = new string[0];
                foreach (KeyValuePair<string, Friendship> pair in Game1.player.friendshipData.Pairs)
                {
                    

                    string name = pair.Key;
                    Friendship friendship = pair.Value;
               

                    var friend = Game1.getCharacterFromName(name);
                    Game1.player.talkToFriend(friend);


      
                }

            }

        }

    }
}