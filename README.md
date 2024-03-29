# Knight Adventure

유니티 첫 프로젝트입니다.  
유니티의 기본기능을 공부하면서 개발한 게임입니다.  

## 목차
- [개요](#개요)  
- [게임설명](#게임설명)
- [게임플레이](#게임플레이)  

## 개요
- 프로젝트 이름 : Knight Adventure
- 프로젝트 개발기간 : 2021.08 - 2021.08
- 개발 엔진 및 언어 : Unity & C#
- 기타 프로그램 : Spine
- 맴버 : 남기산

## 게임설명
|![image](https://github.com/yongmen20/project_Knight/assets/148856359/692d5744-ed43-42b2-91c8-fbc627db5a9b)|![image](https://github.com/yongmen20/project_Knight/assets/148856359/f3879ac4-6715-4ef4-8631-1a610e80b733)|
|---|---|
|<p align="center">메인화면|<p align="center">플레이화면|  

Unity2D의 기능을 이용하여 만들어본 간단한 프로젝트입니다.  
플레이어는 눈앞의 적들을 물리치며 앞으로 나아가야 하며 골인 지점에 도착하면 게임이 종료됩니다.  

### 게임 규칙
|![image](https://github.com/yongmen20/project_Knight/assets/148856359/c0760466-5c13-4d00-9845-034359dc02c7)|![image](https://github.com/yongmen20/project_Knight/assets/148856359/73a18504-208f-4481-8c70-3b1bcad48873)|![image](https://github.com/yongmen20/project_Knight/assets/148856359/a5f1c437-f588-4e21-937f-eed2355be4e2)|![image](https://github.com/yongmen20/project_Knight/assets/148856359/958aed52-5dec-4184-bdcb-5ce750ffdbf5)|
|---|---|---|---|
|<p align="center">플레이어 조작|<p align="center">게임클리어|<p align="center">게임오버조건1|<p align="center">게임오버조건2|  

- **플레이어 조작**
1. START 버튼을 눌러 새로운 게임을 시작하고 만약 저장된 기록이 있으면 CONTINUE 버튼을 눌러 이어하기를 실행한다.  
2. 플레이어는 방향키 또는 점프키(Space Bar)를 이용해 캐릭터를 조작한다.
3. 플레이어는 Z키를 눌러 근접공격을 수행할 수 있다.

- **게임클리어**
1. 플레이어가 맵 끝에 존재하는 출입구(Exit)에 도착한다.  
2. 플레이어가 출입구(Exit)에 접촉하면 클리어 문구와 함께 게임을 종료한다.  

- **게임오버조건1**
1. 플레이어가 맵에 존재하는 낭떠러지에 추락한다.  
2. 플레이어가 다시 올라올 수 없는 위치까지 추락하면 플레이어의 체력을 0으로 하고 게임을 종료 시킨다.

- **게임오버조건2**
1. 플레이어가 적이나 데미지가 존재하는 장애물에 충돌한다.
2. 플레이어의 체력이 0이 아닐 시 약간의 무적시간을 부여한다.
3. 만약 플레이어의 체력이 0이면 게임을 종료 시킨다.  

## 게임플레이
- **플레이어 조작**
  
|이동방향|좌(LEFT)|우(RIGHT)|공격(ATTACK)|
|---|---|---|---|
|<p align="center">키보드|<p align="center">A|<p align="center">D|<p align="center">Z|

- **플레이 영상**
  
https://github.com/yongmen20/project_Knight/assets/148856359/19d9903f-b172-410d-8c76-0f3310d069d9




  
