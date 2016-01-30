using UnityEngine;
using System.Collections;

public abstract class GameState {
	public abstract void Enter();
	public abstract void Exit();
	public abstract void Update();
}
