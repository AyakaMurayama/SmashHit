using UnityEngine;
using System.IO;
using System.Collections;
using SocialConnector; //ライブラリ継承
using UnityEngine.UI;

namespace SocialConnector
{

    /// <summary>
    /// シェアするためのスクリプト
    /// </summary>

    public class ShareController : MonoBehaviour
    {

        public void Share()
        {
            StartCoroutine(_Share());  //押されたらシェアするための処理を始める
        }

        public IEnumerator _Share()
        {
            string imgPath = Application.persistentDataPath + "/image.png";///image.pngという名前で保存

            // 前回のデータを削除
            File.Delete(imgPath);

            //スクリーンショットを撮影
            ScreenCapture.CaptureScreenshot("image.png");

            // 撮影画像の保存が完了するまで待機
            while (true)
            {
                if (File.Exists(imgPath)) break;
                yield return null;
            }

            // 投稿する
            string tweetText = "今回のスコアはこれ！";
            string tweetURL = "";//後々サイト作ったら入れる
            SocialConnector.Share(tweetText, tweetURL, imgPath);
        }
    }
}