using UnityEngine;
using System.Collections.Generic;
 
public enum GameStateId {
	OniricSplash,
	TitleScreen, PlayerTurn,
	GoodTurn, BadTurn,
	Summon, Finished,
	Summary
}
 
public class StateManager {
 
    private static StateManager _instance = null;	

    public GameState gameState { get; private set; }
	private Dictionary<GameStateId, GameState> stateMap;
    
    protected StateManager() {}

    // Singleton pattern implementation
    public static StateManager Instance { 
        get {
            if (StateManager._instance == null) {
                StateManager._instance = new StateManager();
                StateManager._instance.Build();
            }  
            return StateManager._instance;
        } 
    }

    public void ChangeGameState(GameStateId to) {
    	this.gameState.Exit();
		this.gameState = this.stateMap[to];
    	this.gameState.Enter();
    }

	private void Build() {
		this.stateMap = new Dictionary<GameStateId, GameState>();

		this.stateMap.Add(GameStateId.OniricSplash, new OniricSplashState());
		this.stateMap.Add(GameStateId.TitleScreen, new TitleScreenState());
		this.stateMap.Add(GameStateId.PlayerTurn, new PlayerTurnState());
		this.stateMap.Add(GameStateId.GoodTurn, new GoodTurnState());
		this.stateMap.Add(GameStateId.BadTurn, new BadTurnState());
		this.stateMap.Add(GameStateId.Summon, new SummonState());
		this.stateMap.Add(GameStateId.Summary, new SummaryState());

		this.gameState = this.stateMap[GameStateId.TitleScreen];
		// this.gameState = this.stateMap[GameStateId.OniricSplash];
		this.gameState.Enter();
	}

}
