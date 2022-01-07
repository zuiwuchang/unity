using UnityEngine;
namespace com.king011
{
    public enum Adapter
    {
        // 適配高度 高度可以完整顯示 且不會在上下留下黑邊
        Height,
        // 適配寬度 寬度可以完整顯示 且不會在左右留下黑邊
        Width,
        // 比例誤差小於一定值時 縮放目標 否則適配高度，橫向遊戲建議使用此值
        // abs(ratio/screenRatio-1) < scale
        ScaleHeight,
        // 比例誤差小於一定值時 縮放目標 否則適配寬度，豎直遊戲建議使用此值
        // abs(ratio/screenRatio-1) < scale
        ScaleWidth,
    }
    public class Snapshot
    {
        public Camera target;
        public int width;
        public int height;
        public Adapter adapter;
        public float scale;

        public int screenWidth;
        public int screenHeight;

        public void Save(Camera target, Adapter adapter,
            float width, float height,
            float screenWidth, float screenHeight,
            float scale)
        {
            this.target = target;
            this.adapter = adapter;
            this.width = (int)width;
            this.height = (int)height;
            this.screenWidth = (int)screenWidth;
            this.screenHeight = (int)screenHeight;
            this.scale = scale;
        }
        public bool IsNotChanged(Camera target, Adapter adapter,
            float width, float height,
            int screenWidth, int screenHeight,
            float scale)
        {
            return this.target == target && this.adapter == adapter
                && this.screenWidth == screenWidth && this.screenHeight == screenHeight
                && this.width == (int)width && this.height == (int)height
                && Mathf.Approximately(this.scale, scale);
        }
    }
    public class CameraAdapter : MonoBehaviour
    {
        [Label("適配的攝像機")]
        public Camera target;
        [Label("設計寬度")]
        public float width = 1080;
        [Label("設計高度")]
        public float height = 1920;
        [Label("適配模式")]
        public Adapter adapter = Adapter.ScaleWidth;
        [Label("允許縮放的臨界值")]
        public float scale = 0.2f;

        private Snapshot snapshot = new Snapshot();
        // 此函數用於適配攝像機必要時縮放遊戲根節點
        public void FitCamera()
        {
            if (snapshot.IsNotChanged(target, adapter, width, height, Screen.width, Screen.height, scale))
            {
                return;
            }

            // 計算攝像機 設計大小
            var orthographicSize = height / 100 / 2; // 1920/100/2=9.6

            // 計算寬高比
            var ratio = width / height;
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            var screenRatio = screenWidth / screenHeight;

            transform.localScale = new Vector3(1, 1, 1); //恢復縮放，之前的適配策略可能破壞了此值

            if (Mathf.Approximately(ratio, screenRatio))
            {
                // 屏幕 寬/高 和設計相似 無需適配
                target.orthographicSize = orthographicSize;
                snapshot.Save(target, adapter, width, height, screenWidth, screenHeight, scale);
                return;
            }
            var mode = adapter;
            if (mode == Adapter.ScaleHeight)
            {
                if (Scale(ratio, screenRatio))
                {
                    target.orthographicSize = orthographicSize;
                    snapshot.Save(target, adapter, width, height, screenWidth, screenHeight, scale);
                    return;
                }
                mode = Adapter.Height;
            }
            else if (mode == Adapter.ScaleWidth)
            {
                if (Scale(ratio, screenRatio))
                {
                    target.orthographicSize = orthographicSize;
                    snapshot.Save(target, adapter, width, height, screenWidth, screenHeight, scale);
                    return;
                }
                mode = Adapter.Width;
            }

            if (mode == Adapter.Height)
            {
                // 適配高度 無需額外操作
                target.orthographicSize = orthographicSize;
                snapshot.Save(target, adapter, width, height, screenWidth, screenHeight, scale);

                // 如果屏幕比較寬 screenRatio > ratio，會在屏幕左右兩側留下黑邊
                // 如果屏幕比較窄 screenRatio < ratio，不會有黑邊但寬度顯示不全
                return;
            }

            // mode == Adapter.Height
            // 適配寬度 調整攝像機
            target.orthographicSize = orthographicSize * ratio / screenRatio;
            snapshot.Save(target, adapter, width, height, screenWidth, screenHeight, scale);

            // 如果屏幕比較寬 screenRatio > ratio，會縮小攝像機大小，所以不會有黑邊但高度顯示不全
            // 如果屏幕比較窄 screenRatio < ratio，會放大攝像機大小，會在屏幕上下留下黑邊
        }
        private void Awake()
        {
            FitCamera();
        }
        private void Update()
        {
            FitCamera();
        }
        private bool Scale(float ratio, float screenRatio)
        {
            var value = ratio / screenRatio - 1;
            if (value <= scale && value >= -scale)// 屏幕和設計誤差在允許範圍內 
            {
                // 水平拉伸或縮放
                transform.localScale = new Vector3(screenRatio / ratio, 1, 1);
                return true;
            }
            return false;
        }
    }
}