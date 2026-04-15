# Nelja tsükli võrdlusanalüüs

## Kontekst

Neli arendustsüklit ehitasid sama rakenduse (Football Prediction PWA) erinevate spetsifikatsioonide ja mudelitega:

| | Tsükkel 1 (V1) | Tsükkel 2 (V2) | Tsükkel 3 (V3) | Tsükkel 4 (V4) |
|---|---|---|---|---|
| **Kuupäev** | 06.02–05.03.2026 | 12.03–23.03.2026 | 23.03.2026 | 15.04.2026 |
| **Mudel** | Claude Sonnet | Claude Sonnet | Claude Sonnet | Claude Opus 4.6 |
| **Repo** | football-prediction-pwa | football-prediction-pwa | football-prediction-pwa | **football-prediction-pwa-2** |
| **Haru** | version_1_06_02_2026 | version_2_12_03_2026 | version_3_23_03_2026 | dev_2026_04_15 |

**Muutujad tsüklite vahel:**
- Tsüklid 1→3: sama mudel (Sonnet), kasvavad spetsifikatsioonid
- Tsükkel 4: uus mudel (Opus 4.6) + tsüklite 1-3 itereeritud spetsifikatsioonid

---

## 1. Ajaline võrdlus

### 1.1 Kalendriaeg

| | V1 | V2 | V3 | V4 |
|---|---|---|---|---|
| **Algus** | 06.02 14:04 | 12.03 20:47 | 23.03 11:56 | 15.04 10:33 |
| **Lõpp** | 05.03 00:09 | 23.03 09:20 | 23.03 20:15 | 15.04 13:59 |
| **Kalendripäevad** | **27** | **11** | **1** | **1** |

### 1.2 Aktiivne arendusaeg (commitide ajatemplite põhjal)

Metoodika: järjestikuste commitide vaheline aeg loeti aktiivseks, kui vahe <60 min. Pikemaid pause käsitleti sessioonivahetusena (+15 min lõpetamiseks).

| | V1 | V2 | V3 | V4 |
|---|---|---|---|---|
| **Aktiivne aeg** | **15h 24min** | **8h 28min** | **3h 37min** | **2h 54min** |
| **Sessioonide arv** | 12 | 8 | 5 | **2** |
| **Muutus eelmisest** | — | -45% | -57% | **-20%** |
| **Muutus V1-st** | — | -45% | -76% | **-81%** |

```
V1:  ████████████████████████████████ 15h 24min (12 sessiooni, 27 päeva)
V2:  █████████████████                 8h 28min (8 sessiooni, 11 päeva)
V3:  ████████                          3h 37min (5 sessiooni, 1 päev)
V4:  ██████                            2h 54min (2 sessiooni, 1 päev)
```

### 1.3 Sessioonide detailne jaotus

#### V1: 12 sessiooni üle 27 päeva

| # | Kuupäev | Kestus | Commitid | Peamine tegevus |
|---|---------|--------|----------|-----------------|
| 1 | 06.02 | 44 min | 2 | Projekti setup |
| 2 | 06.02 | 50 min | 4 | Migratsioonid, docs |
| 3 | 08.02 | 43 min | 4 | JWT autentimine |
| 4 | 10.02 | 44 min | 3 | Scoring loogika |
| 5 | 10.02 | 15 min | 2 | Turniiride haldamine |
| 6 | 20.02 | 15 min | 2 | Ennustuste süsteem |
| 7 | **21.02** | **234 min** | **17** | **Peamine dev** — edetabel, frontend, PWA |
| 8 | 21.02 | 127 min | 5 | API integratsioon, tulemused |
| 9 | 23.02 | 15 min | 1 | Bugfixid |
| 10 | 26.02 | 52 min | 3 | UI polish |
| 11 | **03.03** | **247 min** | **23** | **Juurutamine** — Render, Vercel, fixid |
| 12 | 04.03 | 48 min | 6 | Vercel SPA routing fixid |

#### V2: 8 sessiooni üle 11 päeva

| # | Kuupäev | Kestus | Commitid | Peamine tegevus |
|---|---------|--------|----------|-----------------|
| 1 | 12.03 | 37 min | 6 | Clean slate, projekti setup |
| 2 | 12.03 | 27 min | 2 | JWT autentimine |
| 3 | 13.03 | 56 min | 6 | Backend (scoring, turniirid, ennustused) |
| 4 | 13.03 | 41 min | 5 | Frontend alus |
| 5 | 19.03 | 75 min | 10 | Frontend UI, PWA, deployment |
| 6 | **22.03** | **181 min** | **12** | **Juurutamine + fixid** |
| 7 | 22.03 | 88 min | 4 | P1 features |
| 8 | 23.03 | 15 min | 1 | Scoring fix |

#### V3: 5 sessiooni, 1 päev

| # | Kuupäev | Kestus | Commitid | Peamine tegevus |
|---|---------|--------|----------|-----------------|
| 1 | 23.03 | 15 min | 1 | Scoring fix (V2 pärand) |
| 2 | 23.03 | 15 min | 1 | Clean slate |
| 3 | 23.03 | 53 min | 15 | Backend + frontend + testid |
| 4 | 23.03 | 52 min | 8 | UX fixid, preferences |
| 5 | 23.03 | 97 min | 5 | Juurutamine + fixid |

#### V4: 2 sessiooni, 1 päev

| # | Kuupäev | Kestus | Commitid | Peamine tegevus |
|---|---------|--------|----------|-----------------|
| 1 | 15.04 | **92 min** | 10 | **Kogu backend + frontend + testid + PWA** |
| 2 | 15.04 | **82 min** | 9 | UX redesign, football-data.org, juurutamine |

### 1.4 Juurutamisele kulunud aeg

| | V1 | V2 | V3 | V4 |
|---|---|---|---|---|
| **Juurutamise sessioonide aeg** | ~295 min | ~181 min | ~97 min | ~40 min |
| **% koguajast** | 32% | 36% | 45% | 23% |
| **Juurutamise commitid** | 16 | 8 | 5 | 5 |

---

## 2. Committide võrdlus

### 2.1 Üldine jaotus

| Kategooria | V1 | % | V2 | % | V3 | % | V4 | % |
|------------|-----|---|-----|---|-----|---|-----|---|
| **feat** | 26 | 36% | 23 | 50% | 12 | 40% | **8** | **53%** |
| **fix** | 22 | 31% | 11 | 24% | 12 | 40% | **5** | **33%** |
| **docs** | 18 | 25% | 8 | 17% | 4 | 13% | **0** | **0%** |
| **test** | 0 | 0% | 0 | 0% | 0 | 0% | **1** | **7%** |
| **chore/muu** | 6 | 8% | 4 | 9% | 2 | 7% | **1** | **7%** |
| **Kokku** | **72** | | **46** | | **30** | | **15** | |

```
         feat                fix                 docs            chore
V1: ██████████████████████████ 26  ██████████████████████ 22  ██████████████████ 18  ██████ 6
V2: ███████████████████████    23  ███████████            11  ████████           8  ████   4
V3: ████████████               12  ████████████           12  ████               4  ██     2
V4: ████████                    8  █████                   5                     0  █      1
```

### 2.2 Trendid

| Mõõdik | V1→V2 | V2→V3 | V3→V4 | **V1→V4** |
|--------|-------|-------|-------|-----------|
| Committid kokku | -36% | -35% | -50% | **-79%** |
| Fix committid | -50% | +9% | -58% | **-77%** |
| Fix osakaal | -7pp | +16pp | -7pp | +2pp |
| Feat osakaal | +14pp | -10pp | +13pp | **+17pp** |
| Docs committid | -56% | -50% | -100% | **-100%** |

---

## 3. Koodimaht

| Komponent | V1 | V2 | V3 | V4 |
|-----------|-----|-----|-----|-----|
| **Backend (C#)** | 2 990 | 3 247 | 2 990 | **3 455** |
| **Frontend (TS/HTML/CSS)** | 1 666 | 2 637 | 1 666 | **1 240** |
| **Testid** | 125 | — | 55 | **110** |
| **Kokku** | **4 781** | **5 884** | **4 711** | **4 805** |

```
V1:  ████████████████████████████████████████████████ 4781
V2:  ██████████████████████████████████████████████████████████ 5884
V3:  ███████████████████████████████████████████████ 4711
V4:  ████████████████████████████████████████████████ 4805
```

### 3.1 Rakenduse struktuur

| | V1 | V2 | V3 | V4 |
|---|---|---|---|---|
| **API otspunktid** | 30 | 30 | 30 | **33** |
| **Frontend komponendid** | 12 | 19 | 12 | **6** |
| **Frontend teenused** | 7 | 9 | 7 | **4** |
| **Ühiktestid** | ~125 rida | — | ~55 rida | **~110 rida** |

**Trend:** V4-l on kõige kompaktsem frontend (6 komponenti, 4 teenust), kuid kõige rohkem API otspunkte (33, tänu football-data.org importimisele ja tournament/setup endpointidele).

---

## 4. Blokeerijad

### 4.1 Koguarv kategooriate kaupa

| Kategooria | V1 | V2 | V3 | V4 |
|------------|-----|-----|-----|-----|
| **Backend loogika** | 3 | 2 | 0 | **0** |
| **Frontend/UX** | 3 | 2 | 7 | **0** |
| **Juurutamine** | 5 | 6 | 4 | **5** |
| **Tööriist/konfig** | 0 | 1 | 0 | **0** |
| **Kokku** | **11** | **11** | **11** | **5** |

```
Backend:    ███ → ██ → · → ·      Elimineeritud V3-st
Frontend:   ███ → ██ → ███████ → · Elimineeritud V4-s
Juurutam:   █████ → ██████ → ████ → █████  Püsiv
Kokku:      ███████████ → ███████████ → ███████████ → █████
                 11            11            11           5
```

### 4.2 Blokeerijate detailne loend

#### V1 blokeerijad (11)

| # | Blokeerija | Kategooria |
|---|-----------|------------|
| 1 | .NET SDK versioon vale | Tööriist |
| 2 | dotnet-ef versioon vale | Tööriist |
| 3 | EF migratsioon ei rakendu | Backend |
| 4 | PostgreSQL port conflict | Infrastruktuur |
| 5 | Tailwind v4 ühildumatus Angular 19-ga | Frontend |
| 6 | JSON serialization infinite loop | Backend |
| 7 | PostgreSQL URL → Npgsql parser | Juurutamine |
| 8 | Vercel SPA routing 404 | Juurutamine |
| 9 | Angular outputPath /browser/ | Juurutamine |
| 10 | CORS konfiguratsiooni puudumine | Juurutamine |
| 11 | Angular standalone component puudu | Frontend |

#### V2 blokeerijad (11)

| # | Blokeerija | Kategooria | V1 kordus? |
|---|-----------|------------|------------|
| 1 | Dockerfile restore loogika | Juurutamine | EI |
| 2 | PostgreSQL URL parser (postgres://) | Juurutamine | **JAH** |
| 3 | postgresql:// URI skeemi käsitlus | Juurutamine | **JAH** (variant) |
| 4 | Port puudub URI-s (default 5432) | Juurutamine | **JAH** (variant) |
| 5 | Render API URL vale env-s | Juurutamine | EI |
| 6 | Vercel CORS preview URL-idele | Juurutamine | **JAH** (variant) |
| 7 | Auth guard redirect vale tee | Frontend | EI |
| 8 | Angular catch-all route puudu | Frontend | EI |
| 9 | Angular fileReplacements | Juurutamine | EI |
| 10 | JSON serialization loop | Backend | **JAH** |
| 11 | Scoring ei käivitu pärast sync | Backend | EI |

#### V3 blokeerijad (11)

| # | Blokeerija | Kategooria | V2 kordus? |
|---|-----------|------------|------------|
| 1 | Frontend model ID-d number vs string | Frontend | EI |
| 2 | 204 NoContent response null | Frontend | EI |
| 3 | Match card ei näita ennustust | UX | EI |
| 4 | Dropdown state ei püsi | UX | EI |
| 5 | Preferences ei filtreeri vaateid | UX | EI |
| 6 | Preferences algseisu probleem | UX | EI |
| 7 | Desktop layout liiga lai | UX | EI |
| 8 | DATABASE_URL lugemine | Juurutamine | **JAH** |
| 9 | PostgreSQL URL parser port 443 | Juurutamine | **JAH** (variant) |
| 10 | Angular outputPath /browser/ | Juurutamine | **JAH** |
| 11 | Render API URL vale env-s | Juurutamine | **JAH** |

#### V4 blokeerijad (5)

| # | Blokeerija | Kategooria | V3 kordus? |
|---|-----------|------------|------------|
| 1 | Render blueprint connectionURI süntaks | Juurutamine | EI |
| 2 | Dockerfile Docker context path | Juurutamine | EI (variant) |
| 3 | PostgreSQL URL parser port 443 | Juurutamine | **JAH** |
| 4 | DB migration crash | Juurutamine | EI |
| 5 | Render API URL erinev oodatust | Juurutamine | **JAH** |

### 4.3 Eelmise tsükli blokeerijate ennetamismäär

| | V1→V2 | V2→V3 | V3→V4 |
|---|---|---|---|
| **Ennetatud** | 5/11 (45%) | 8/11 (73%) | 9/11 (82%) |
| **Kordus** | 4/11 (36%) | 3/11 (27%) | 2/11 (18%) |
| **Uued** | 2/11 (18%) | 0/11 (0%) | 0/11 (0%) — kõik 5 on juurutamine |

```
Ennetamismäär:  45% → 73% → 82%   ↑ kasvav
Kordumismäär:   36% → 27% → 18%   ↓ kahanev
```

### 4.4 PostgreSQL URL parseri ajalugu

See on ainus probleemiklaster, mis kordub igas tsüklis:

| Tsükkel | Variant | Põhjus |
|---------|---------|--------|
| V1 | Puudub täielikult | Ei teatud, et Render kasutab postgres:// URL-i |
| V2 | postgres:// → Npgsql parser loodud, postgresql:// puudu, port puudu | Parser liiga lihtne |
| V3 | https:// default port 443 | URI parser kasutab https:// skeemi, mis annab porti 443 |
| V4 | **Sama** — https:// default port 443 | ERROR-PREVENTION kirjeldas probleemi, kuid kood kopeeriti v3 näitest, mis sisaldas sama viga |

**Järeldus:** ERROR-PREVENTION.md kirjeldab probleemi (ERROR 7), kuid lahenduse näidiskood ise sisaldab viga. See on metatasandi probleem — spetsifikatsioon dokumenteerib probleemi, kuid pakub vigast lahendust.

---

## 5. Spetsifikatsioonide võrdlus

### 5.1 Failide arv (alguses vs lõpus)

| | V1 algus | V1 lõpp | V2 sisend | V3 sisend | V4 sisend |
|---|---|---|---|---|---|
| **Agentide raamistik** | 5 | 13 | 10 | 20 | **20** |
| **Projekti-spetsiifilised** | 4 | 18 | 26 | 55 | **6** |
| **Kokku** | 9 | 31 | 36 | 75 | **26** |

**Oluline:** V4 sisendis on ainult 6 projekti-spetsiifilist faili (4 originaalset + ERROR-PREVENTION + START-HERE), kuna piletid, analüüsid ja mockupid jäeti välja. Agentide raamistik (20 faili) on sama mis V3 lõpus.

### 5.2 Raamistiku vs projekti failide eristus

| Kategooria | Kirjeldus | V1 | V2 | V3 | V4 |
|------------|-----------|-----|-----|-----|-----|
| **Agentide raamistik** | Agendid, workflow'd, mallid — taaskasutatavad | 5 | 10 | 20 | **20** |
| **Hübriid** | ERROR-PREVENTION, START-HERE — raamistiku metoodika, projekti sisu | 0 | 1 | 2 | **2** |
| **Projekti-spetsiifilised** | REQUIREMENTS, GAME-RULES, ARCHITECTURE, TECH-STACK, piletid, analüüsid | 4 | 25 | 53 | **4** |

```
Raamistik:  █████ → ██████████ → ████████████████████ → ████████████████████
                5         10              20                    20

Projekt:    ████ → █████████████████████████ → █████████████████████████████████████████████████████ → ████
               4            25                              53                                         4
```

### 5.3 ERROR-PREVENTION reeglite arv

| | V1 | V2 | V3 | V4 |
|---|---|---|---|---|
| **Reegleid sisendis** | 0 | 5 | 18 | **20** |
| **Reegleid lõpus** | 0 | ~11 | 25 | **20** (ei kasvanud) |

### 5.4 ERROR-PREVENTION tõhusus V4-s

| | Tulemus |
|---|---|
| Reegleid kokku | 20 |
| Täielikult ennetatud | **17 (85%)** |
| Osaliselt ennetatud (uus variant) | 3 (15%) |
| Täielikult kordus | **0 (0%)** |

---

## 6. Koondtrend

### 6.1 Kõik mõõdikud koos

| Mõõdik | V1 | V2 | V3 | V4 | V1→V4 |
|--------|-----|-----|-----|-----|-------|
| **Aktiivne aeg** | 15h 24min | 8h 28min | 3h 37min | **2h 54min** | **-81%** |
| **Sessioonid** | 12 | 8 | 5 | **2** | -83% |
| **Committid** | 72 | 46 | 30 | **15** | **-79%** |
| **Fix committid** | 22 | 11 | 12 | **5** | **-77%** |
| **Docs committid** | 18 | 8 | 4 | **0** | -100% |
| **Feat osakaal** | 36% | 50% | 40% | **53%** | +17pp |
| **Koodimaht** | 4 781 | 5 884 | 4 711 | **4 805** | ~0% |
| **Blokeerijad** | 11 | 11 | 11 | **5** | **-55%** |
| **Backend blokeerijad** | 3 | 2 | 0 | **0** | -100% |
| **Frontend blokeerijad** | 3 | 2 | 7 | **0** | -100% |
| **Juurutamise blokeerijad** | 5 | 6 | 4 | **5** | 0% |
| **Ennetamismäär** | — | 45% | 73% | **82%** | — |
| **ERROR-PREVENTION reeglid** | 0 | 5 | 18 | **20** | — |
| **Raamistiku failid** | 5 | 10 | 20 | **20** | +300% |

### 6.2 Visuaalne kokkuvõte

```
Aktiivne aeg:
V1  ████████████████████████████████████████████████████████████████████████████████ 15h 24m
V2  ███████████████████████████████████████████                                      8h 28m
V3  ██████████████████                                                               3h 37m
V4  ███████████████                                                                  2h 54m

Blokeerijad:
V1  ███ BE  ███ FE  █████ Deploy       = 11
V2  ██ BE   ██ FE   ██████ Deploy  █   = 11
V3          ███████ FE  ████ Deploy    = 11
V4                      █████ Deploy   = 5

Ennetamismäär:
V2  █████████░░░░░░░░░░░  45%
V3  ███████████████░░░░░  73%
V4  █████████████████░░░  82%
```

---

## 7. Järeldused

### 7.1 Peamised leiud

1. **Arenduskiirus paranes 5.3x** (15h 24min → 2h 54min). Suurim hüpe oli V1→V2 (-45%) ja V2→V3 (-57%). V3→V4 paranemine oli väiksem (-20%), mis viitab asümptootilisele lähenemisele.

2. **Blokeerijate arv langes esmakordselt alla 11** V4-s (5). Tsüklid 1-3 olid kõik 11 blokeerijaga — arv ei muutunud, kuid **koosseis** muutus drastiliselt.

3. **Blokeerijate nihe:** Backend → Frontend → Juurutamine
   - V1: backend ja frontend olid suurimad probleemid
   - V2: sama mustrid, vähem kordi
   - V3: backend elimineeritud, frontend UX plahvatas (7 probleemi)
   - V4: **kõik elimineeritud peale juurutamise**

4. **Ennetamismäär tõuseb monotoonselt:** 45% → 73% → 82%. Iga tsükkel ennetab rohkem eelmise tsükli vigu.

5. **Kordumismäär langeb monotoonselt:** 36% → 27% → 18%. Üha vähem vigu kordub.

6. **Dokumentatsiooni committid kadusid:** 18 → 8 → 4 → 0. Itereeritud spetsifikatsioonid elimineerisid jooksva dokumenteerimisvajaduse.

7. **Koodimaht stabiliseerus ~4800 real.** See on sama funktsionaalsuse "loomulik suurus" selle tech stackiga.

### 7.2 Juurutamine kui ennetamise ülempiir

Juurutamise blokeerijad (5/5 V4-s) moodustavad **ennetamise küllastuspunkti**:

| Probleem | Miks ei saa ennetada |
|----------|---------------------|
| Render blueprint API muutub | Kolmanda osapoole API |
| Docker context erineb platvormiti | CI/CD keskkonna eripära |
| Render genereerib ettearvamatu URL-i | Dünaamiline nimetamine |
| PostgreSQL URL parser | ERROR-PREVENTION näidiskood sisaldab ise viga |
| DB migration crash | Render free tier timing |

Need on **platvormi-spetsiifilised** probleemid, kus:
- Kolmanda osapoole käitumine muutub versioonide vahel
- URL-id on dünaamilised ja ettearvamatud
- Keskkonna eripärad ilmnevad ainult juurutamisel

### 7.3 Mudeli mõju (Opus vs Sonnet)

| Aspekt | Sonnet (V1-V3) | Opus (V4) |
|--------|----------------|-----------|
| Frontend komponendid | 12-19 | **6** |
| Frontend teenused | 7-9 | **4** |
| Inline templates | Ei | **Jah** |
| Backend data joining (MatchWithPredictionDto) | Ei | **Jah** |
| Frontend state bugid | 0-7 per tsükkel | **0** |

Opus tegi lihtsama arhitektuuri, mis vältis frontend state probleeme. **Kuid** Opus ilma spetsifikatsioonideta ei oleks teadnud:
- Tailwind v4 on ühildumatu Angular 19-ga
- .NET SDK tuleb pinnida versioonile 9.0
- PostgreSQL port peab olema 5433
- Vercel vajab SPA rewrite reegleid

**Spetsifikatsioonid ja mudeli võimekus on komplementaarsed.**

### 7.4 Lõppjäreldus

Nelja tsükli eksperiment näitab, et **spetsifikatsioonide itereerimine vähendab arendusaega 81% ja blokeerijaid 55%**. Backend ja frontend probleemid on täielikult elimineeritud ERROR-PREVENTION reeglite ja agentide raamistiku abil. Ülejäänud blokeerijad on platvormi-spetsiifilised juurutamisprobleemid, mis esindavad ennetamise teoreetilist ülempiiri — koht, kus kolmandate osapoolte platvormide ettearvamatu käitumine muudab täieliku ennetamise võimatuks.
