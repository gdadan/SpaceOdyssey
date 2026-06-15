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

## 🛠 1. 페이지 스냅 중첩 스크롤 — 직접 구현한 탭 스와이프 UI

### 고민했던 점
로비는 **상점 · 인벤토리 · 플레이 · 스킬트리** 4개 화면을 가로로 넘기는 구조였습니다. Unity 기본 `ScrollRect`만으로는 (1) 페이지가 어중간한 위치에 멈추고, (2) 가로로 넘긴 뒤 세로 스크롤이 이전 위치에 남아있는 문제가 있었습니다. "카카오톡 하단 탭처럼 딱딱 붙는 느낌"을 직접 만들어야 했습니다.

### 해결법
`IBeginDragHandler / IDragHandler / IEndDragHandler`를 구현해 드래그를 직접 제어하고, **페이지 스냅 + 플릭 감지 + 부드러운 보간**을 더했습니다.

**(1) 페이지 위치를 0~1로 정규화해 미리 계산**
```csharp
const int size = 4;                 // 페이지(탭) 개수
float[] pos = new float[size];
float distance;                     // 페이지 사이 간격

void Start()
{
    distance = 1f / (size - 1);     // 4개면 0, 0.33, 0.66, 1
    for (int i = 0; i < size; i++)
        pos[i] = distance * i;
}
```

**(2) 손을 떼면 가장 가까운 페이지로 스냅 + 빠르게 튕기면(flick) 페이지 이동**
```csharp
public void OnEndDrag(PointerEventData e)
{
    targetPos = SetPos();           // 가장 가까운 페이지로 스냅

    // 절반을 안 넘겨도 빠르게 스와이프하면 한 페이지 이동
    if (curPos == targetPos)
    {
        if (e.delta.x > 18  && curPos - distance >= 0)      // 왼쪽 플릭
        { targetIndex--; targetPos = curPos - distance; }
        if (e.delta.x < -18 && curPos + distance <= 1.01f)  // 오른쪽 플릭
        { targetIndex++; targetPos = curPos + distance; }
    }
    VerticalScrollUp();             // 페이지 전환 시 세로 스크롤을 맨 위로 리셋
}

float SetPos()                      // 현재 위치에서 가장 가까운 페이지 반환
{
    for (int i = 0; i < size; i++)
        if (scrollbar.value < pos[i] + distance * 0.5f &&
            scrollbar.value > pos[i] - distance * 0.5f)
        { targetIndex = i; return pos[i]; }
    return 0;
}
```

**(3) 손을 뗀 뒤 부드럽게 이동 + 선택된 탭 버튼 애니메이션**
```csharp
void Update()
{
    if (!isDragging)
    {
        scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);  // 부드럽게 정렬

        // 선택된 탭은 가로로 커지고, 아이콘은 중앙으로 이동 + 텍스트 표시
        for (int i = 0; i < size; i++)
            btnRect[i].sizeDelta = new Vector2(i == targetIndex ? 360 : 180,
                                               btnRect[i].sizeDelta.y);
    }
}
```

→ 외부 에셋 없이 **스냅 · 플릭 · 보간 · 탭 강조 · 중첩 스크롤 리셋**까지 직접 구현한, 이 프로젝트에서 가장 공들인 UI입니다.

🔗 [NestedScrollManager.cs](./Assets/UI_AH/Scripts/UI/Manager/NestedScrollManager.cs)

<br/>

---

## 🛠 2. 오브젝트 풀링 — 총알 재사용으로 성능 최적화

### 고민했던 점
슈팅 게임은 총알이 **초당 수십 개씩 생성·파괴**됩니다. 매번 `Instantiate`/`Destroy`를 하면 GC 부담과 프레임 드랍이 생겨, 모바일에서는 특히 치명적이었습니다.

### 해결법
총알 종류별로 `Queue`를 두고, **비활성화로 회수 → 재사용**하는 오브젝트 풀을 만들었습니다.

```csharp
public List<Queue<GameObject>> bullets;  // 총알 종류별 풀

public GameObject GetObj(int num)
{
    if (bullets[num].Count > 0)          // 풀에 남은 게 있으면 꺼내 재사용
    {
        var obj = bullets[num].Dequeue();
        obj.SetActive(true);
        return obj;
    }
    return Create(num);                   // 없을 때만 새로 생성
}

public void ReturnObj(int num, GameObject go)
{
    go.SetActive(false);                 // 비활성화 후 풀로 반환
    bullets[num].Enqueue(go);
}
```

→ 런타임 생성/파괴를 최소화해 **GC 스파이크 없이 안정적인 프레임**을 확보했습니다.

🔗 [ObjectPoolUI.cs](./Assets/UI_AH/Scripts/UI/Lobby/ObjectPoolUI.cs)

<br/>

---

## 🛠 3. 비동기 씬 로딩 — 자연스러운 로딩바

### 고민했던 점
씬을 `LoadScene`으로 한 번에 불러오면 화면이 멈췄다가 툭 전환됩니다. 로딩바가 0.9에서 갑자기 끝나버리는 어색함도 해결하고 싶었습니다.

### 해결법
`LoadSceneAsync` + `allowSceneActivation = false`로 **로딩이 끝나도 전환을 미루고**, 0.9~1 구간을 1초에 걸쳐 채워 자연스럽게 마무리했습니다.

```csharp
IEnumerator LoadSceneProcess()
{
    AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
    op.allowSceneActivation = false;          // 로딩 끝나도 자동 전환 막기

    float timer = 0f;
    while (!op.isDone)
    {
        yield return null;
        if (op.progress < 0.9f)
            progressBar.fillAmount = op.progress;
        else                                  // 0.9~1은 1초에 걸쳐 채우기
        {
            timer += Time.unscaledDeltaTime;
            progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
            if (progressBar.fillAmount >= 1f)
            {
                op.allowSceneActivation = true;  // 다 채워지면 전환
                yield break;
            }
        }
    }
}
```

🔗 [LoadingSceneController.cs](./Assets/UI_AH/Scripts/LoadingSceneController.cs)

<br/>

---

## 🛠 4. 모바일 컨트롤 — 2종 조작 방식과 UI 터치 무시

### 고민했던 점
플레이어마다 선호하는 조작이 달라, **'터치 지점을 따라가기'와 '손가락 드래그'** 두 방식을 모두 지원하고 토글할 수 있게 하고 싶었습니다. 또 상점 버튼을 누르려다 비행기가 움직이는 오작동도 막아야 했습니다.

### 해결법
`controllType` 값으로 조작 방식을 분기하고, `EventSystem.IsPointerOverGameObject`로 **UI 위 터치는 무시**했습니다.

```csharp
public int controllType;            // 0: 따라가기, 1: 드래그

void PlayerMove()
{
    // 터치가 없거나 UI 위를 눌렀으면 무시 → 버튼 오작동 방지
    if (Input.touchCount < 1 || EventSystem.current.IsPointerOverGameObject(0))
        return;

    Touch touch = Input.GetTouch(0);
    Vector3 worldPos = GetInputPosition(touch.position);

    if (controllType == 0)          // 따라가기: 터치 지점 방향으로 이동
        moveDir = GetDirection(transform.position, worldPos);
    else                            // 드래그: 손가락이 움직인 만큼 평행 이동
    {
        Vector2 delta = (Vector2)worldPos - firstTouchPos;
        transform.position += new Vector3(delta.x, delta.y, 0);
        firstTouchPos = worldPos;
    }
}
```

> 📌 PC(에디터)와 모바일을 `Application.platform`으로 구분해, 빌드 타깃에 맞는 입력만 동작하도록 처리했습니다. *(핵심 로직만 발췌)*

🔗 [PlayerController.cs](./Assets/UI_AH/Scripts/PlayerController.cs)

<br/>

---

## 🛠 5. 해상도 대응 — 다양한 기기에서 9:16 비율 고정

### 고민했던 점
모바일은 기기마다 화면 비율이 제각각이라, 그대로 두면 UI가 늘어나거나 잘렸습니다. 어떤 기기에서도 **9:16 비율을 유지**해야 했습니다.

### 해결법
카메라의 `Rect`를 화면 비율에 맞춰 조정해 **레터박스(위아래)/필러박스(좌우)** 를 만들고, 남는 영역은 검게 칠했습니다.

```csharp
void Start()
{
    Camera cam = GetComponent<Camera>();
    Rect rect = cam.rect;
    float scaleHeight = ((float)Screen.width / Screen.height) / (9f / 16f);

    if (scaleHeight < 1)            // 화면이 더 길쭉 → 위아래 레터박스
    {
        rect.height = scaleHeight;
        rect.y = (1f - scaleHeight) / 2f;
    }
    else                           // 더 넓음 → 좌우 필러박스
    {
        rect.width = 1f / scaleHeight;
        rect.x = (1f - rect.width) / 2f;
    }
    cam.rect = rect;
}

void OnPreCull() => GL.Clear(true, true, Color.black);  // 남는 영역은 검게
```

🔗 [CameraResolution.cs](./Assets/UI_AH/Scripts/CameraResolution.cs)

<br/>

---

## 🛠 6. TSV 데이터 파싱 — 데이터와 코드의 분리

### 고민했던 점
아이템 능력치를 코드에 하드코딩하니, 수치 하나 바꿀 때마다 코드를 고치고 다시 빌드해야 했습니다. **기획자도 수정할 수 있게** 데이터를 외부로 빼고 싶었습니다.

### 해결법
멘토에게 질문하며 **TSV(탭 구분) 시트를 `Resources`에서 읽어 파싱**하는 로더를 처음 구현했습니다. 헤더 이름으로 열을 찾아, 행/열을 이름으로 조회합니다.

```csharp
public void Create(string file)                 // Resources의 TSV 로드
{
    TextAsset data = Resources.Load(file) as TextAsset;
    rowtext = data.text.Split("\n");
}

// num번째 행에서 'column' 이름의 값을 조회
public string GetString(int num, string column)
{
    string[] cells = rowtext[num].Split("\t");
    return cells[GetColumn(column)];            // 헤더에서 열 인덱스를 찾아 매칭
}
```

→ 이후 모든 프로젝트에서 **데이터-코드 분리**를 기본으로 적용하게 된 출발점이었습니다.

🔗 [TsvLoader.cs](./Assets/UI_AH/Scripts/UI/Lobby/TsvLoader.cs)

<br/>

---

## 💡 회고 — 이 프로젝트에서 배운 것

- **첫 협업 프로젝트** : 혼자 만들 때는 몰랐던 "다른 사람과 코드를 합치는 것"의 어려움과 중요성을 배웠습니다. 파트(UI)를 책임지고 끝까지 맡으면서 **역할 분담과 인터페이스 합의**의 중요성을 체감했습니다.
- **데이터 외부화의 가치** : 멘토에게 질문하며 TSV 파싱을 처음 구현했고, 이후 모든 프로젝트에서 데이터-코드 분리를 기본으로 적용하게 됐습니다.
- **모바일 UX** : 화면 비율, 터치 입력, UI 위 터치 무시 처리 등 **모바일만의 디테일**을 고민하는 계기가 됐습니다.
- **"기본 컴포넌트의 한계를 직접 넘어보기"** : `ScrollRect`로 부족했던 부분을 드래그 핸들러로 직접 구현하며, **엔진이 주는 것에 머물지 않고 필요한 동작을 만들어내는** 경험을 했습니다.

<br/>

## 📁 코드 구조 (담당 파트 발췌)

```
Assets/UI_AH/Scripts/
├── PlayerController.cs              # 2종 모바일 조작 + UI 터치 무시
├── CameraResolution.cs             # 9:16 비율 고정 (레터박스/필러박스)
├── LoadingSceneController.cs       # 비동기 씬 로딩 + 로딩바
└── UI/
    ├── Manager/
    │   ├── NestedScrollManager.cs  # 페이지 스냅 중첩 스크롤 (핵심)
    │   ├── ShopManager.cs          # 상점
    │   ├── InventoryManager.cs     # 인벤토리
    │   ├── SkilltreeManager.cs     # 스킬트리
    │   └── DataManager.cs          # 보유 데이터 관리
    └── Lobby/
        ├── ObjectPoolUI.cs         # 총알 오브젝트 풀링
        ├── TsvLoader.cs            # TSV 외부 데이터 파싱
        └── Slot/                   # 아이템·총알·스킬 슬롯 UI
```
