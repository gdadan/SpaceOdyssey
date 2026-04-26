# 스페이스 오디세이

충북 게임 아카데미 프로젝트부 멘토-멘티 팀프로젝트 과정에서 만든 슈팅 게임입니다.

> **🏆 2023 충북게임 아카데미 프로젝트부 성과발표회 최우수상**

<br/>

## 📝 프로젝트 소개

| 항목 | 내용 |
|------|------|
| **개발 기간** | 2023.08 ~ 2023.10 (3개월) |
| **개발 환경** | C#, Unity |
| **개발 장르** | 2D 모바일 슈팅 게임 |
| **개발 인원** | 프로그래머 3명 (UI 파트 단독 담당) |

**한 줄 설명** : 비행기 부품을 모아 점점 강해져 모든 스테이지를 클리어해야 하는 모바일 슈팅 게임입니다.

🔗 [담당 작업 폴더 (UI_AH)](./Assets/UI_AH)

<br/>

## 📷 인게임 화면

<img src="https://github.com/user-attachments/assets/03478852-d60a-4828-8036-533ef9698249" width="16%" />
<img src="https://github.com/user-attachments/assets/06e3603e-eb4e-48b9-a309-e19008add157" width="16%" />
<img src="https://github.com/user-attachments/assets/da9dd54e-930b-4531-b104-1d4d7a8bcb50" width="16%" />
<img src="https://github.com/user-attachments/assets/3d826df0-8aae-4787-9bf1-2da3a9c154eb" width="16%" />
<img src="https://github.com/user-attachments/assets/dbe31d67-3cb7-46c0-bb27-ad098d629077" width="16%" />
<img src="https://github.com/user-attachments/assets/86d1a330-d1a6-4ece-acc9-dfe6396a4297" width="16%" />

<br/>

## 🎯 담당 작업 요약

| 영역 | 핵심 키워드 |
|------|------------|
| **UI 파트 전담** | 타이틀 / 로딩 / 인게임 상점 / 로비(상점·인벤토리·스킬트리) |
| **모바일 입력** | 두 가지 컨트롤 타입(따라가기 / 드래그) 토글 가능 |
| **비동기 로딩** | `AsyncOperation` + `allowSceneActivation` 활용한 자연스러운 씬 전환 |
| **다양한 해상도 대응** | 카메라 Rect 조정으로 9:16 비율 강제 유지 |
| **데이터 관리** | TSV 외부 시트 파싱으로 아이템 데이터 관리 |
| **협업** | 3인 팀 Git 협업 |

<br/>

---

## 💡 회고 — 이 프로젝트에서 배운 것

- **첫 협업 프로젝트** : 혼자 만들 때는 몰랐던 "다른 사람과 코드를 합치는 것"의 어려움과 중요성을 배웠습니다.
- **데이터 외부화의 가치** : 멘토에게 질문하며 TSV 파싱을 처음 구현했고, 이후 모든 프로젝트에서 데이터-코드 분리를 기본으로 적용하게 됐습니다.
- **모바일 UX** : 화면 비율, 터치 입력, UI 위 터치 무시 처리 등 **모바일만의 디테일**을 고민하는 계기가 됐습니다.
