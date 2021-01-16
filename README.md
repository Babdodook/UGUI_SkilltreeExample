# 아키에이지 스킬트리 시스템

아키에이지의 스킬트리를 모작하여 구현하였습니다.
스킬 포인트의 분배, 스킬 초기화, 다른 능력 선택 등이 가능합니다.
능력은 총 5개가 구현되어 있습니다.

# 개발환경
* Unity 2019.2.21
* Visual Studio 2019

# 기능
* 능력 선택
* 능력 변경
* 능력 초기화
* 스킬 정보

# 시스템 구조
![구조1](https://user-images.githubusercontent.com/48229283/104789265-d64eb680-57d7-11eb-9aec-72e012a26d4d.PNG)  
매니저가 모든 능력을 관리하고
능력마다 스킬정보를 가지고 있는 단순한 구조입니다.  
Json으로 스킬의 정보를 파싱해서 관리하며, 새로운 스킬을 추가할 때 Json정보만 업데이트 시켜주면 됩니다.  

# 기능 설명
## 1. 능력 선택

이미지1 | 이미지2
:-------------------------:|:-------------------------:
![능력 선택1](https://user-images.githubusercontent.com/48229283/104789486-7f95ac80-57d8-11eb-9c9d-d023f6a83c7d.PNG) | ![능력 선택2](https://user-images.githubusercontent.com/48229283/104789489-815f7000-57d8-11eb-8b35-ba254049ac4d.PNG)

능력은 5개가 구현되어있습니다.
능력을 선택하여 해당 능력의 정보를 불러올 수 있습니다.

## 2. 능력 변경

이미지1 | 이미지2
:-------------------------:|:-------------------------:
![능력 변경1](https://user-images.githubusercontent.com/48229283/104790453-90dfb880-57da-11eb-8ddf-22a4dc4a8eac.PNG) | ![능력 변경2](https://user-images.githubusercontent.com/48229283/104789853-75c07900-57d9-11eb-938f-aa6fefaa2334.PNG)

스킬셋 변경 버튼을 눌러 다른 능력으로 변경할 수 있습니다.  
이미 선택된 스킬셋으로는 변경할 수 없습니다.  

## 3. 능력 초기화

이미지1 | 이미지2
:-------------------------:|:-------------------------:
![능력 초기화](https://user-images.githubusercontent.com/48229283/104790536-cdabaf80-57da-11eb-90c8-3aa32243ced9.PNG) | ![능력 초기화2](https://user-images.githubusercontent.com/48229283/104790540-cf757300-57da-11eb-87b1-57005449f65e.PNG)

초기화 버튼을 눌러 스킬 포인트를 초기화 할 수 있습니다.

## 4. 스킬 정보

이미지1 | 이미지2
:-------------------------:|:-------------------------:
![스킬정보1](https://user-images.githubusercontent.com/48229283/104790751-7ce88680-57db-11eb-935b-17b268e91679.png) | ![스킬정보2](https://user-images.githubusercontent.com/48229283/104790755-7eb24a00-57db-11eb-9a86-23a3cf64c5d3.png)

마우스 커서를 올리면 해당 스킬의 정보를 열람할 수 있습니다.

# Code

