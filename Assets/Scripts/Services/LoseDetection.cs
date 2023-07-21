using UnityEngine;
using Fusion;

public class LoseDetection:NetworkBehaviour
{    
    private int diedCount;
    private bool isDied;
    private bool isLosed;

    private Character character;
    private GameOverUI gameOverUI;
    private WaveSystem waveSystem;

    public void InitDependecies(Character character,GameOverUI gameOverUI,WaveSystem waveSystem)
    {
        this.character = character;
        this.gameOverUI = gameOverUI;
        this.waveSystem = waveSystem;
    }

    public void RemotePlayerLeave()
    {
        if (this.isDied)
            RPC_CharacterDied();
        else if(diedCount == 0)
            diedCount++;
    }

    private void Update()
    {
        if (!isDied && character.IsAlive == false)
        {
            RPC_CharacterDied();
            isDied = true;
        }

    }

    [Rpc(sources: RpcSources.All, targets: RpcTargets.All)]
    private void RPC_CharacterDied()
    {
        diedCount++;

        if (HasStateAuthority && diedCount == FusionConnect.PlayersCount && isLosed == false && !waveSystem.IsWin)
            RPC_LoseGame();
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    private void RPC_LoseGame()
    {
        isLosed = true;
        gameOverUI.OpenLose();
        waveSystem.LoseGame();
    }

}