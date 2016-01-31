using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TotalScore {
	public int totalActionsPerformed;
	public int totalActionsExpected;
	public int totalTurns;
	public float sumRatio;

	public TotalScore() {
		this.totalActionsPerformed = 0;
		this.totalActionsExpected = 0;
		this.totalTurns = 0;
		this.sumRatio = 0.0f;
	}
}

public class TurnInfo {
	public int numActionsPerformed;
	public int numActionsExpected;
	public ArrayList buttons;
	public float timePerformed;
	public float timeExpected;
	public int currentIdx;

	public TurnInfo() {
		this.numActionsPerformed = 0;
		this.numActionsExpected = 0;
		this.buttons = new ArrayList();
		this.timePerformed = 0.0f;
		this.timeExpected = 0.0f;
		this.currentIdx = 0;
	}
}

public class TurnEnd {
	public bool successful;

	public TurnEnd() {
		this.successful = false;
	}
}

public enum GameEndType {
	NotYet, NoFollowers, TurnLimitReached
};

public class Game : MonoBehaviour {

    // Singleton pattern implementation
    private static Game _instance = null;	
    protected Game() {}
    public static Game Instance { 
        get {
            if (Game._instance == null) {
            	Debug.Log("instance");
            	GameObject obj = new GameObject();
                Game._instance = obj.AddComponent<Game>();
                Game._instance.objUi = GameObject.FindWithTag("objui").GetComponent<ObjUi>();
                Game._instance.following = GameObject.FindWithTag("following").GetComponent<Following>();
                Game._instance.shouldStateUpdate = true;
                Game._instance.summoner = GameObject.FindWithTag("summoner").GetComponent<Summoner>();
                Game._instance.defaultCameraSize = Camera.main.orthographicSize;
                Game._instance.player = GameObject.FindWithTag("Player");
                Game._instance.playerAnimator = Game._instance.player.GetComponent<Animator>();
                Game._instance.playerAudio = Game._instance.player.GetComponent<AudioSource>();
				Game._instance.player.SetActive(false);
				Game._instance.circleGuiObj = GameObject.FindWithTag("circle_gui");
				Game._instance.circleGuiProgress = GameObject.FindWithTag("circle_time").GetComponent<CiclularProgress>();
				Game._instance.circleGuiObj.SetActive(false);
            }  
            return Game._instance;
        } 
    }

    public void Start() {
    	// hack to start singleton instance before player deactivates
    	// since we need to get the players gameobject by tag
    	#pragma warning disable 219
		Game a = Game.Instance;
    	#pragma warning restore 219
    }

	private bool shouldStateUpdate = false;
	public TurnInfo turnInfo = new TurnInfo();
	public TotalScore totalScore = new TotalScore();
	private TurnEnd turnEnd = new TurnEnd();
	public bool isPlayerTurn = false;
	public ObjUi objUi;
	public Following following;
	public float currentRatio = 0.0f;
	public Summoner summoner;
	public bool stunned = false;
	public GameEndType gameEndType = GameEndType.NotYet;
	public float defaultCameraSize = 0.0f;
	public GameObject player;
	public Animator playerAnimator;
	public AudioSource playerAudio;
	public GameObject circleGuiObj;
	public CiclularProgress circleGuiProgress;

	private void Update () {
		// due to the singleton, there are two updates for game running :v
		if(shouldStateUpdate) {
			StateManager.Instance.gameState.Update();
		}
	}

	public TurnEnd CalculateTurnSuccess(TurnEndCheck tec) {
		this.turnEnd.successful = false;

		switch(tec) {
			// turn time is up, fill the rest of the buttons with failure
			case TurnEndCheck.OutOfTime:
				// fallthrough
				goto case TurnEndCheck.CompletedActions;
			case TurnEndCheck.CompletedActions:
				float CCC = (float) this.turnInfo.currentIdx;
				float EEE = (float) this.turnInfo.numActionsExpected;
				float PPP = (float) this.turnInfo.numActionsPerformed;
				float ratioTop = CCC;
				float ratioBottom = 0.0f;
				// E > P
				if(this.turnInfo.numActionsExpected > this.turnInfo.numActionsPerformed) {
					ratioBottom = EEE + (EEE - PPP);
				}
				// E < P
				else if (this.turnInfo.numActionsExpected < this.turnInfo.numActionsPerformed){
					ratioBottom = EEE + (PPP - EEE) * Difficulty.imperfectModifier;
				}
				// E == P
				else {
					ratioBottom = EEE;
				}
				ratioBottom *= Difficulty.ratioModifier;

				this.currentRatio = ratioTop / ratioBottom;
				this.totalScore.sumRatio += this.currentRatio;

				// Debug.Log("e." + EEE + " -- p; + PPP + " -- c." + CCC);
				// Debug.Log("ratio: " + this.currentRatio + " = " + ratioTop + " / " + ratioBottom);
				Debug.Log("ratio: " + this.currentRatio);
				if(this.currentRatio > Difficulty.ratioToTurnSuccess) {
					this.turnEnd.successful = true;
				}
				break;
			case TurnEndCheck.NotYet:
				Debug.Log("turn hasnt ended. shouldnt calculate success");
				Debug.Break();
				break;
			default:
				Debug.Log("unknown turn end check");
				Debug.Break();
				break;
		}

		return this.turnEnd;
	}

	public GameEndType IsGameEnd() {
		if(this.following.Size() <= 0) {
			this.gameEndType = GameEndType.NoFollowers;
		}
		else if(this.totalScore.totalTurns >= Difficulty.totalTurns) {
			this.gameEndType = GameEndType.TurnLimitReached;
		}
		else {
			this.gameEndType = GameEndType.NotYet;
		}
		
		return this.gameEndType;
	}

	public void ResetTurn() {
		if(this.totalScore.totalTurns == 0) {
			Game.Instance.FirstTurn();
		}

		this.totalScore.totalActionsPerformed += this.turnInfo.numActionsPerformed;
		this.totalScore.totalActionsExpected += this.turnInfo.numActionsExpected;
		++this.totalScore.totalTurns;

		this.currentRatio = 0.0f;
		this.turnEnd = new TurnEnd();

		this.turnInfo.numActionsExpected = Difficulty.NumActionsTurn(this.totalScore.totalTurns);
		this.turnInfo.timeExpected = Difficulty.SecondsInTurn(this.turnInfo.numActionsExpected,
				this.totalScore.totalTurns);
	
		this.turnInfo.numActionsPerformed = 0;
		this.turnInfo.timePerformed = 0.0f;
		this.turnInfo.currentIdx = 0;

		this.turnInfo.buttons.Clear();
	}

	public void ReceiveAction(Action.ButtonId btn) {
		++this.turnInfo.numActionsPerformed;
		DrawId drawId = DrawId.MISS;
		// correct
		if((Action.ButtonId)this.turnInfo.buttons[this.turnInfo.currentIdx] == btn) {
			// Debug.Log("OK(" + this.turnInfo.numActionsPerformed + ")! " + btn + " idx " + this.turnInfo.currentIdx);
			++this.turnInfo.currentIdx;
			drawId = DrawId.HIT;
		}
		// wrong
		else {
			// Debug.Log("ERR(" + this.turnInfo.numActionsPerformed + ")! " + btn + " idx " + this.turnInfo.currentIdx);
			// objUi
			drawId = DrawId.MISS;
			this.stunned = true;
			this.SetText("MISS! STUNNED");
			this.playerAnimator.Play("miss input");
			this.player.GetComponent<Player>().PlayClip();
		}
		DrawButtons(drawId);
	}

	public void DrawButtons(DrawId drawId) {
		this.objUi.Clear();
		this.objUi.Begin();

		for(int i = 0; i < this.turnInfo.buttons.Count; ++i) {
			// current button
			if(i == this.turnInfo.currentIdx) {
				if(drawId == DrawId.MISS) {
					this.objUi.Add(this.objUi.btn_bg_red, (Action.ButtonId)this.turnInfo.buttons[i], true);
				}
				else {
					this.objUi.Add(this.objUi.btn_bg_black, (Action.ButtonId)this.turnInfo.buttons[i]);
				}
			}
			// completed buttons
			else if (i < this.turnInfo.currentIdx){
				this.objUi.Add(this.objUi.btn_bg_green, (Action.ButtonId)this.turnInfo.buttons[i], false, false);
			}
			// future buttons
			else if (i > this.turnInfo.currentIdx + 3){
				this.objUi.Add(this.objUi.btn_bg_black, (Action.ButtonId)this.turnInfo.buttons[i], false, false);
			}
			else {
				this.objUi.Add(this.objUi.btn_bg_black, (Action.ButtonId)this.turnInfo.buttons[i]);
			}
		}
	}

	public void FirstTurn() {
		this.following.Add(6);
	}

	public bool turnOver = false;
	public bool TriggerWaitForTurnOver() {
		this.isPlayerTurn = false;
		StartCoroutine("WaitForSecsTurnOver", 2.0f);
		return this.turnOver;
	}

	public void StopWaitForTurnOver() {
		StopCoroutine("WaitForSecsTurnOver");
	}

	IEnumerator WaitForSecsTurnOver(float seconds) {
		// this.objUi.ClearCloud();
		yield return new WaitForSeconds(seconds);
		// Game.Instance.SetText("done!");
		this.turnOver = true;
	}

	public void TriggerWaitForStun() {
		StartCoroutine("WaitForSecsStunOver", Difficulty.stunDuration);
	}
	public void StopWaitForStunOver() {
		StopCoroutine("WaitForSecsStunOver");
	}

	IEnumerator WaitForSecsStunOver(float seconds) {
		this.SetText("stunned");
		yield return new WaitForSeconds(seconds);
		CancelStun();
		this.SetText("go");
	}

	public void CancelStun() {
		this.stunned = false;
		DrawButtons(DrawId.NEUTRAL);
	}

	public void PlaySummonAnimation() {
		this.playerAnimator.Play("summoning");
	}

	public void SummonCreature() {
		GameObject summonObj = this.GetSummonBasedOnPoints();
		Instantiate(summonObj);		
	}

	public bool goodSummon = false;

	private GameObject GetSummonBasedOnPoints() {
		GameObject chosenSummonObj = null;

		if(this.gameEndType == GameEndType.NotYet) {
			Debug.Log("game should not have ended.");
			Debug.Break();
			chosenSummonObj = null;
		}

		if(this.gameEndType == GameEndType.NoFollowers) {
			chosenSummonObj = this.summoner.bad_summon;
		} else if(this.gameEndType == GameEndType.TurnLimitReached) {
			float bestRatio = this.totalScore.totalTurns * 1.0f;
			float sumRatio = this.totalScore.sumRatio;
			float sumRatioToSuccess = this.totalScore.totalTurns * Difficulty.ratioToTurnSuccess;

			Debug.Log("best possible: " + bestRatio);
			Debug.Log("sum ratio: " + sumRatio);
			Debug.Log("sum success: " + sumRatioToSuccess);

			this.goodSummon = (sumRatio/bestRatio) > Difficulty.ratioToGoodSummon;
			if(this.goodSummon) {
				chosenSummonObj = this.summoner.good_summons[0];
			}
			else {
				chosenSummonObj = this.summoner.neutral_summons[0];
			}
		}

		return chosenSummonObj;
	}

	public void SetText(string str) {
		// this.objUi.helpText.text = str;
		// ►
	}

	public void ResetAll() {
		// reset all game. new game, without restarting.
	}
}
