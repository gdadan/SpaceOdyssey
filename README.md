# 스페이스 오디세이

충북 게임 아카데미 프로젝트부 멘토-멘티 팀프로젝트 과정에서 만든 슈팅 게임입니다.

> **🏆 2023 충북게임 아카데미 프로젝트부 성과발표회 최우수상**

<br/>

## 📝 프로젝트 소개

| 항목 | 내용 |
|------|------|
| **개발 기간** | 2023.08 ~ 2023.10 (3개월) |
| **개발 환경** | C#, Unity 2022.3 LTS |
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
| **협업** | 3인 팀 Git 협업, 머지 컨플릭트 해결 |

<br/>

---

## 🛠 1. 모바일 터치 컨트롤 — 두 가지 조작 방식 토글

### 무엇을 (What)
플레이어가 선호하는 조작 방식을 선택할 수 있도록 **두 가지 컨트롤 타입**을 구현했습니다.
- **타입 0** : 터치 지점으로 비행기가 따라가는 방식
- **타입 1** : 드래그한 만큼 비행기가 이동하는 방식

### 왜 (Why)
모바일 슈팅 게임은 사용자마다 손에 익은 조작이 달라, 한 가지 방식만 강제하면 이탈률이 높아짐. 옵션으로 토글할 수 있어야 했습니다.

### 어떻게 (How)
`controllType` 변수 하나로 `Update` 안에서 분기 처리하고, **터치 단계별(Began / Stationary / Moved / Ended)**로 상태를 관리했습니다.

```csharp
if (Input.GetTouch(0).phase == TouchPhase.Began)
{
    if (controllType == 0)
    {
        // 따라가기: 터치 지점이 멀면 isFar = true (방향 이동)
        if (Vector3.Distance(transform.position, inputPosition) > .4f)
            isFar = true;
    }
    else
    {
        // 드래그: 첫 터치 지점만 기억
        firstTouchPos = GetInputPosition(Input.GetTouch(0).position);
    }
}
```

- **EventSystem 체크** : `IsPointerOverGameObject()`로 UI 위 터치는 이동에서 제외 → 버튼 누를 때 비행기가 움직이지 않음
- **플랫폼 분기** : 안드로이드/iOS에서만 터치 입력 처리, 에디터에서는 마우스로 테스트
- **방향 정규화** : `Atan2`로 각도 계산 후 `cos/sin`으로 단위 벡터 추출 → 일정한 속도 보장

🔗 [PlayerController.cs](./Assets/UI_AH/Scripts/PlayerController.cs)

<br/>

---

## 🚀 2. 로딩 씬 — 비동기 씬 로드와 자연스러운 진행도 표시

### 무엇을 (What)
스테이지 전환 시 끊김 없이 보이도록 **비동기 로딩 씬**을 구현했습니다.

### 왜 (Why)
- `SceneManager.LoadScene` 동기 호출은 **로딩 중 화면이 멈춰** 사용자 경험이 나쁨
- Unity의 `AsyncOperation.progress`는 0.9에서 멈추기 때문에 그대로 쓰면 **로딩바가 항상 90%에서 멈춰 보이는 문제**가 있음

### 어떻게 (How)
`allowSceneActivation = false`로 자동 활성화를 막고, 0.9 이후 구간은 **수동 보간(Lerp)**으로 채워 자연스러운 100% 도달을 연출했습니다.

```csharp
AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
op.allowSceneActivation = false;

while (!op.isDone)
{
    if (op.progress < 0.9f)
        progressBar.fillAmount = op.progress;
    else
    {
        // 0.9 → 1로 1초에 걸쳐 부드럽게 채우기
        timer += Time.unscaledDeltaTime;
        progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
        if (progressBar.fillAmount >= 1f)
        {
            op.allowSceneActivation = true;
            yield break;
        }
    }
    yield return null;
}
```

- **`Time.unscaledDeltaTime` 사용** : 게임 일시정지(timeScale=0) 상태에서도 로딩바가 정상 동작
- **`static` 메서드 활용** : `LoadingSceneController.LoadScene("StageName")` 한 줄로 어디서든 호출 가능

🔗 [LoadingSceneController.cs](./Assets/UI_AH/Scripts/LoadingSceneController.cs)

<br/>

---

## 📱 3. 다양한 해상도 대응 — 카메라 Rect 동적 조정

### 무엇을 (What)
모든 모바일 기기에서 게임이 9:16 비율로 보이도록 **레터박스/필러박스 자동 적용** 시스템을 만들었습니다.

### 왜 (Why)
모바일은 18:9, 19.5:9, 20:9 등 화면 비율이 천차만별. UI/게임 오브젝트가 늘어나거나 잘려 보이면 게임 밸런스가 깨짐.

### 어떻게 (How)
화면 비율을 계산해 카메라 `rect`의 너비/높이를 줄이고, 남는 공간은 검은색으로 클리어.

```csharp
float scaleheight = ((float)Screen.width / Screen.height) / ((float)9 / 16);
float scalewidth = 1f / scaleheight;

if (scaleheight < 1)  // 화면이 더 길쭉할 때 → 위아래 검은 띠
{
    rect.height = scaleheight;
    rect.y = (1f - scaleheight) / 2f;
}
else                  // 화면이 더 넓을 때 → 좌우 검은 띠
{
    rect.width = scalewidth;
    rect.x = (1f - scalewidth) / 2f;
}

void OnPreCull() => GL.Clear(true, true, Color.black);  // 남는 영역 검정 처리
```

🔗 [CameraResolution.cs](./Assets/UI_AH/Scripts/CameraResolution.cs)

<br/>

---

## 🛒 4. UI 파트 전담 — 매니저 패턴 기반 화면 구성

타이틀, 로딩, 로비(상점/인벤토리/스킬트리), 인게임 상점, 일시정지, 부활 팝업까지 **UI 전 영역**을 단독으로 설계·구현했습니다.

| 화면 | 핵심 스크립트 |
|------|--------------|
| 인벤토리 매니저 | [`InventoryManager.cs`](./Assets/UI_AH/Scripts/UI/Manager/InventoryManager.cs) |
| 로비 (상점/스킬트리) | [Lobby/](./Assets/UI_AH/Scripts/UI/Lobby) |
| 매니저 클래스들 | [Manager/](./Assets/UI_AH/Scripts/UI/Manager) |
| 부활 팝업 | [`RevivePopUp.cs`](./Assets/UI_AH/Scripts/UI/RevivePopUp.cs) |
| 일시정지 | [`Pause.cs`](./Assets/UI_AH/Scripts/UI/Pause.cs) |

<br/>

---

## 📊 5. TSV 외부 데이터로 아이템 관리

### 왜 (Why)
아이템 종류와 수치가 자주 바뀌는 게임 특성상, **하드코딩하면 밸런싱마다 빌드해야 함**. 또 처음 시도해본 외부 데이터 관리였어요.

### 어떻게 (How)
TSV(Tab-Separated Values) 시트를 파싱해 런타임에 아이템 데이터를 동적으로 로드. 기획자가 시트만 수정하면 게임에 반영되는 구조로 만들었습니다.

→ 이 과정에서 처음으로 **"코드와 데이터의 분리"** 개념을 익혔고, 나중에 회사 프로젝트(섀도우 헌터)에서 시트 기반 스킬 데이터 시스템으로 발전시켰습니다.

<br/>

---

## 🤝 6. 협업 경험 — 3인 팀 Git 워크플로우

### 무엇을 (What)
프로그래머 3명이 동시에 한 Unity 프로젝트를 작업하며 GitHub로 형상관리.

### 마주한 문제
- Unity의 `.scene`, `.prefab`, `.meta` 파일은 **머지 충돌이 매우 자주 발생**
- 누군가 하나 수정하면 다른 팀원의 변경사항이 덮어써질 위험

### 어떻게 해결했나
- **자주 소통, 자주 머지** : 작업 영역을 미리 분담(나는 UI, 다른 팀원은 인게임/서버)해 충돌 가능성을 줄임
- **정기적 통합** : 큰 충돌이 누적되지 않도록 매일 또는 작업 단위로 머지
- **충돌 발생 시 직접 해결** : 어떤 변경사항이 살아남아야 하는지 팀원과 직접 확인 후 수동 병합

→ 이 경험을 통해 **"브랜치 전략 / 작업 분할 / 머지 사이클"의 중요성**을 체득했습니다.

<br/>

---

## 💡 회고 — 이 프로젝트에서 배운 것

- **첫 협업 프로젝트** : 혼자 만들 때는 몰랐던 "다른 사람과 코드를 합치는 것"의 어려움과 중요성을 배웠습니다.
- **데이터 외부화의 가치** : 멘토에게 질문하며 TSV 파싱을 처음 구현했고, 이후 모든 프로젝트에서 데이터-코드 분리를 기본으로 적용하게 됐습니다.
- **모바일 UX** : 화면 비율, 터치 입력, UI 위 터치 무시 처리 등 **모바일만의 디테일**을 고민하는 계기가 됐습니다.

<br/>

## 📁 담당 폴더 구조

```
Assets/UI_AH/
├── Scripts/
│   ├── PlayerController.cs       # 두 가지 모바일 터치 컨트롤
│   ├── LoadingSceneController.cs # 비동기 씬 로딩
│   ├── CameraResolution.cs       # 9:16 비율 강제
│   └── UI/
│       ├── Manager/              # 인벤토리/상점 매니저들
│       ├── Lobby/                # 로비 화면
│       ├── RevivePopUp.cs        # 부활 팝업
│       ├── Pause.cs              # 일시정지
│       └── Title.cs              # 타이틀
├── Prefabs/
├── Scenes/
└── Asset/
```
