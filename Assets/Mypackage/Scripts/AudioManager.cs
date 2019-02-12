using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// BGMとSEの管理をするマネージャ。シングルトン。
/// </summary>
public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    //オーディオファイルのパス
    private const string BGM_PATH = "Audio/BGM";
    private const string SE_PATH = "Audio/SE";

    //BGMがフェードするのにかかる時間
    public const float BGM_FADE_SPEED_RATE_HIGH = 0.9f;
    public const float BGM_FADE_SPEED_RATE_LOW = 0.3f;
    private float _bgmFadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH;

    //次流すBGM名、SE名
    private string _nextBGMName;
    private string _nextSEName;

    //BGMをフェードアウト中か
    private bool _isFadeOut = false;

    //BGM用、SE用に分けてオーディオソースを持つ
    private AudioSource _bgmSource;
    private List<AudioSource> _seSourceList;
    private const int SE_SOURCE_NUM = 10;

    //全AudioClipを保持
    private Dictionary<string, AudioClip> _bgmDic, _seDic;

    private const string BGM_VOLUME_KEY = "BGM_VOLUME_KEY";//音量を保存するためのキー
    private const string SE_VOLUME_KEY = "SE_VOLUME_KEY";//同上
    private const float BGM_VOLUME_DEFULT = 1.0f;
    private const float SE_VOLUME_DEFULT = 1.0f;


    //=================================================================================
    //初期化
    //=================================================================================

    protected override void Awake()

    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);//どのシーンでもこのオブジェクトを保持する

        //オーディオリスナーおよびオーディオソースをSE+1(BGMの分)作成
        gameObject.AddComponent<AudioListener>();//自身にオーディオリスナーを追加
        for (int i = 0; i < SE_SOURCE_NUM + 1; i++)//必要なSEの数だけ
        {
            gameObject.AddComponent<AudioSource>();//オーディオソースを追加
        }

        //作成したオーディオソースを取得して各変数に設定、ボリュームも設定
        AudioSource[] audioSourceArray = GetComponents<AudioSource>();//オーディオソースを変数に格納
        _seSourceList = new List<AudioSource>();//SEのリスト？

        for (int i = 0; i < audioSourceArray.Length; i++)//オーディオソースの数だけ
        {
            audioSourceArray[i].playOnAwake = false;//再生した時にはならないようにする

            if (i == 0)//0個目
            {
                audioSourceArray[i].loop = true;//ループ再生する
                _bgmSource = audioSourceArray[i];//BGMを0個目に登録
                _bgmSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);//BGMの音量設定をする
            }
            else//それ以外
            {
                _seSourceList.Add(audioSourceArray[i]);//順番にSEを登録
                audioSourceArray[i].volume = PlayerPrefs.GetFloat(SE_VOLUME_KEY, SE_VOLUME_DEFULT);//SEの音量設定をする
            }

        }

        //リソースフォルダから全SE&BGMのファイルを読み込みセット
        _bgmDic = new Dictionary<string, AudioClip>();
        _seDic = new Dictionary<string, AudioClip>();

        //
        object[] bgmList = Resources.LoadAll(BGM_PATH);//リソーシーズの中にあるBGMをロード
        object[] seList = Resources.LoadAll(SE_PATH);//リソーシーズの中にあるSEをロード

        foreach (AudioClip bgm in bgmList)
        {
            _bgmDic[bgm.name] = bgm;//流すBGMの名前を取得
        }
        foreach (AudioClip se in seList)
        {
            _seDic[se.name] = se;//流すSEの名前を取得
        }

    }

    //=================================================================================
    //SE
    //=================================================================================

    /// <summary>
    /// 指定したファイル名のSEを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
    /// </summary>
    public void PlaySE(string seName, float delay = 0.0f)//他クラスから音を鳴らす時に呼ばれる
    {
        if (!_seDic.ContainsKey(seName))//もし呼ばれたSE名と同じものがなかった場合
        {
            Debug.Log(seName + "という名前のSEがありません");
            return;//なかったと帰る
        }

        _nextSEName = seName;//SEの名前をseNameにする
        Invoke("DelayPlaySE", delay);//流す処理へ
    }

    private void DelayPlaySE()//流す処理
    {
        foreach (AudioSource seSource in _seSourceList)
        {
            if (!seSource.isPlaying)//もしいま音が鳴ってなかったら
            {
                seSource.PlayOneShot(_seDic[_nextSEName] as AudioClip);//オーディオクリップにSEを登録
                return;
            }
        }
    }

    //=================================================================================
    //BGM
    //=================================================================================

    /// <summary>
    /// 指定したファイル名のBGMを流す。ただし既に流れている場合は前の曲をフェードアウトさせてから。
    /// 第二引数のfadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
    /// </summary>
    public void PlayBGM(string bgmName, float fadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH)//他クラスから音を鳴らす時に呼ばれる
    {
        if (!_bgmDic.ContainsKey(bgmName))//もし呼ばれたBGM名と同じものがなかった場合
        {
            Debug.Log(bgmName + "という名前のBGMがありません");
            return;//なかったと帰る
        }

        //現在BGMが流れていない時はそのまま流す
        if (!_bgmSource.isPlaying)
        {
            _nextBGMName = "";//名前を無しにして
            _bgmSource.clip = _bgmDic[bgmName] as AudioClip;//クリップに登録
            _bgmSource.Play();//鳴らす
        }
        //違うBGMが流れている時は、流れているBGMをフェードアウトさせてから次を流す。同じBGMが流れている時はスルー
        else if (_bgmSource.clip.name != bgmName)
        {
            _nextBGMName = bgmName;//次のBGMの名前を取得
            FadeOutBGM(fadeSpeedRate);//少しずつ消す処理へ
        }
    }

    /// <summary>
    /// BGMをすぐに止める
    /// </summary>
    public void StopBGM()
    {
        _bgmSource.Stop();//止める
    }

    /// <summary>
    /// 現在流れている曲をフェードアウトさせる
    /// fadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
    /// </summary>
    public void FadeOutBGM(float fadeSpeedRate = BGM_FADE_SPEED_RATE_LOW)
    {
        _bgmFadeSpeedRate = fadeSpeedRate;
        _isFadeOut = true;//フェードアウト中である
    }

    private void Update()
    {
        if (!_isFadeOut)//フェードアウト中でなかったら
        {
            return;//そのまま
        }

        //徐々にボリュームを下げていき、ボリュームが0になったらボリュームを戻し次の曲を流す
        _bgmSource.volume -= Time.deltaTime * _bgmFadeSpeedRate;//少しずつ音を小さくしていく
        if (_bgmSource.volume <= 0)//もし音が鳴っていなかったら
        {
            _bgmSource.Stop();//BGMを止める
            _bgmSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);//保存してある元の設定を取得
            _isFadeOut = false;//フェードアウト中でないといする

            if (!string.IsNullOrEmpty(_nextBGMName))//もし次のBGMがあったら
            {
                PlayBGM(_nextBGMName);//流す
            }
        }

    }

    //=================================================================================
    //音量変更
    //=================================================================================

    /// <summary>
    /// BGMとSEのボリュームを別々に変更&保存
    /// </summary>
    public void ChangeVolume(float BGMVolume, float SEVolume)
    {
        _bgmSource.volume = BGMVolume;
        foreach (AudioSource seSource in _seSourceList)
        {
            seSource.volume = SEVolume;
        }

        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, BGMVolume);//BGM_VOLUME_KEYという名前でBGMVolumeの値を保存
        PlayerPrefs.SetFloat(SE_VOLUME_KEY, SEVolume);//SE_VOLUME_KEYという名前でSEVolumeの値を保存
    }

}