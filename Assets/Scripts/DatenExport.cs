using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatenExport : MonoBehaviour
{
   private int UserId;
   private bool Avatar; 
   private bool Score;
   private int NeckScore;
   private int TrunkScore;
   private int LegsScore;
   private int TableAScore;
   private int UpperArmRS;
   private int UpperArmLS;
   private int LowerArmRS;
   private int LowerArmLS;
   private int WristRS;
   private int WristLS;
   private int TableBR;
   private int TableBL;
    private int ScoreOverallR;
   private int ScoreOverallL;

    public DatenExport(int userId, bool avatar, bool score, int neckScore, int trunkScore, int legsScore,  int tableAScore, int upperArmRS, int upperArmLS, int lowerArmRS, int lowerArmLS, int wristRS, int wristLS, int tableBR, int tableBL, int scoreOverallR,int scoreOverallL)
    {
        UserId= userId;
        Avatar= avatar;
        Score= score;
        NeckScore=neckScore;
        TrunkScore= trunkScore;
        LegsScore= legsScore;
        TableAScore = tableAScore;
        UpperArmRS=upperArmRS;
        UpperArmLS=upperArmLS;
        LowerArmRS=lowerArmRS;
        LowerArmLS=lowerArmLS;
        WristRS=wristRS;
        WristLS=wristLS;
        TableBR=tableBR;
        TableBL=tableBL;
        ScoreOverallR=scoreOverallR;
        ScoreOverallL=scoreOverallL;
    }

    public int GetUserId()
    {
        return UserId;
    }


    public bool GetAvatar()
    {
        return Avatar;
    }

    public void SetAvatar(bool value)
    {
        SetAvatar(value);
    }

    public bool GetScore()
    {
        return Score;
    }

    public void SetScore(bool value)
    {
        SetScore(value);
    }

    public int GetNeckScore()
    {
        return NeckScore;
    }

    public void SetNeckScore(int value)
    {
        SetNeckScore(value);
    }

    public int GetTrunkScore()
    {
        return TrunkScore;
    }

    public void SetTrunkScore(int value)
    {
        SetTrunkScore(value);
    }

    public int GetLegsScore()
    {
        return LegsScore;
    }

    public void SetLegsScore(int value)
    {
        SetLegsScore(value);
    }

    public int GetTableAScore()
    {
        return TableAScore;
    }

    public void SetTableAScore(int value)
    {
        SetTableAScore(value);
    }

    public int GetUpperArmRS()
    {
        return UpperArmRS;
    }

    public void SetUpperArmRS(int value)
    {
        SetUpperArmRS(value);
    }

    public int GetUpperArmLS()
    {
        return UpperArmLS;
    }

    public void SetUpperArmLS(int value)
    {
        SetUpperArmLS(value);
    }

    public int GetLowerArmRS()
    {
        return LowerArmRS;
    }

    public void SetLowerArmRS(int value)
    {
        SetLowerArmRS(value);
    }

    public int GetLowerArmLS()
    {
        return LowerArmLS;
    }

    public void SetLowerArmLS(int value)
    {
        SetLowerArmLS(value);
    }

    public int GetWristRS()
    {
        return WristRS;
    }

    public void SetWristRS(int value)
    {
        SetWristRS(value);
    }

    public int GetWristLS()
    {
        return WristLS;
    }

    public void SetWristLS(int value)
    {
        SetWristLS(value);
    }

    public int GetTableBR()
    {
        return TableBR;
    }

    public void SetTableBR(int value)
    {
        SetTableBR(value);
    }

    public int GetTableBL()
    {
        return TableBL;
    }

    public void SetTableBL(int value)
    {
        SetTableBL(value);
    }

    public int GetScoreOverallR()
    {
        return ScoreOverallR;
    }

    public void SetScoreOverallR(int value)
    {
        SetScoreOverallR(value);
    }

    public int GetScoreOverallL()
    {
        return ScoreOverallL;
    }

    public void SetScoreOverallL(int value)
    {
        SetScoreOverallL(value);
    }
}
