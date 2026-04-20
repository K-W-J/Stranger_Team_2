using System.Collections.Generic;
using _01_Work.LCM._01.Scripts.Weather;
using UnityEditor;
using UnityEngine;

namespace _01_Work.LCM._01.Scripts.Editor
{
    [CustomEditor(typeof(WeatherManager))] // WeatherManager 컴포넌트 전용 커스텀 인스펙터
    public class WeatherManagerEditor : UnityEditor.Editor
    {
        // 인스펙터 GUI 재정의 (기본 기능 + 추가 기능)
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI(); // 기본 인스펙터 요소 렌더링

            WeatherManager manager = (WeatherManager)target; // 현재 선택된 객체 참조

            // GUI 버튼 생성 및 클릭 이벤트 처리
            if (GUILayout.Button("WeatherSO 다 넣기"))
            {
                LoadAllWeatherSO(manager); // 버튼 클릭 시 에셋 로드 작업 실행
            }
        }

        /// <summary>
        /// 프로젝트 전체에서 WeatherSO 타입의 모든 스크립터블 오브젝트를 찾아 등록
        /// </summary>
        private void LoadAllWeatherSO(WeatherManager manager)
        {
            // 1. 프로젝트 내 모든 WeatherSO 에셋 GUID 수집
            string[] guids = AssetDatabase.FindAssets("t:WeatherSO");
            
            // 2. 기존 리스트 초기화 (중복 추가 방지)
            manager.weathers = new List<WeatherSO>();

            // 3. GUID 배열 순회 처리
            foreach (string guid in guids)
            {
                // GUID를 실제 에셋 경로로 변환
                string path = AssetDatabase.GUIDToAssetPath(guid);
                
                // 경로 기반 에셋 로드
                WeatherSO so = AssetDatabase.LoadAssetAtPath<WeatherSO>(path);
                
                // 유효성 검사 후 리스트에 추가
                if (so != null)
                {
                    manager.weathers.Add(so);
                }
            }

            // 4. 변경 사항 저장을 위한 마킹
            EditorUtility.SetDirty(manager); 
        }
    }
}
