# Tsükkel 1 vs Tsükkel 4: Otsene võrdlus

## Kontekst

**Tsükkel 1 (V1):** Esimene arenduskatse. Minimaalsed spetsifikatsioonid (8 faili, ~3000 rida). Mudel: Claude Sonnet. Agentide raamistik puudub.

**Tsükkel 4 (V4):** Neljas arenduskatse. Sama projekti sisend (REQUIREMENTS.md, GAME-RULES.md, ARCHITECTURE.md, TECH-STACK.md), kuid täiendatud agentide raamistikuga (13 agenti, 4 workflow'd, ERROR-PREVENTION 20 reeglit), mis ehitati välja tsüklite 1-3 jooksul. Mudel: Claude Opus 4.6 (1M context).

**Muutujad:**
- Spetsifikatsioonid: V1 minimaalne → V4 itereeritud raamistik
- Mudel: Sonnet → Opus 4.6 (oluliselt suurem ja võimekam)
- Mõlemad: sama projekti nõuded, sama juurutuskeskkond (Vercel + Render)

---

## 1. Ajaline võrdlus

### Kalendriaeg

| | V1 | V4 | Muutus |
|---|---|---|---|
| **Algus** | 06.02.2026 14:04 | 15.04.2026 10:33 | — |
| **Lõpp** | 05.03.2026 00:09 | 15.04.2026 13:59 | — |
| **Kalendripäevad** | 27 päeva | **1 päev** | **-96%** |

### Aktiivne arendusaeg (commitide ajatemplite põhjal)

Aktiivse aja arvutamiseks loeti järjestikuste commitide vaheline aeg aktiivseks, kui vahe oli <60 minutit. Pikemaid pause käsitleti sessioonide vahetusena (+15 min sessiooni lõpetamiseks).

| | V1 | V4 | Muutus |
|---|---|---|---|
| **Sessioonide arv** | 12 | **2** | -83% |
| **Aktiivne aeg kokku** | **~15h 24min** | **~2h 54min** | **-81%** |

### V1 sessioonide jaotus (12 sessiooni, 7 aktiivset päeva)

| Sessioon | Kuupäev | Kestus | Commitid | Tegevus |
|----------|---------|--------|----------|---------|
| 1 | 06.02, 14:04 | 44 min | 2 | Projekti setup, backend alus |
| 2 | 06.02, 22:35 | 50 min | 4 | Migratsioonid, dokumentatsioon |
| 3 | 08.02, 14:33 | 43 min | 4 | Autentimine (JWT) |
| 4 | 10.02, 18:49 | 44 min | 3 | Scoring loogika |
| 5 | 10.02, 20:43 | 15 min | 2 | Turniiride haldamine |
| 6 | 20.02, 17:31 | 15 min | 2 | Ennustuste süsteem |
| 7 | 21.02, 08:36 | **234 min** | 17 | **Peamine arenduspäev** — edetabel, frontend, PWA |
| 8 | 21.02, 15:08 | 127 min | 5 | API integratsioon, tulemused |
| 9 | 23.02, 00:06 | 15 min | 1 | Kriitilised bugfixid |
| 10 | 26.02, 22:25 | 52 min | 3 | Navigatsioon, UI polish |
| 11 | 03.03, 20:30 | **247 min** | 23 | **Juurutamine** — Render, Vercel, bugfixid |
| 12 | 04.03, 23:36 | 48 min | 6 | Vercel SPA routing fixid |

### V4 sessioonide jaotus (2 sessiooni, 1 päev)

| Sessioon | Kuupäev | Kestus | Commitid | Tegevus |
|----------|---------|--------|----------|---------|
| 1 | 15.04, 10:33 | **92 min** | 10 | **Kogu backend + frontend + testid + PWA** |
| 2 | 15.04, 12:51 | **82 min** | 9 | UX ümberdisain, football-data.org, juurutamine, fixid |

**Märkimisväärne:** V1 juurutamise sessioon üksi (247 min, 23 committid) oli pikem kui kogu V4 arendus (174 min, 19 committid).

---

## 2. Committide võrdlus

| Kategooria | V1 | % | **V4** | **%** | Muutus |
|------------|-----|---|--------|-------|--------|
| **feat** | 26 | 36% | **8** | **53%** | -69% arv, +17pp osakaal |
| **fix** | 22 | 31% | **5** | **33%** | **-77%** |
| **docs** | 18 | 25% | **0** | **0%** | **-100%** |
| **test** | 0 | 0% | **1** | **7%** | Uus |
| **chore/muu** | 6 | 8% | **1** | **7%** | -83% |
| **Kokku** | **72** | 100% | **15** | 100% | **-79%** |

### Fix-ide detailne võrdlus

**V1 fixid (22 committid):**
- Backend: PostgreSQL migratsioon, API validatsioon, serialisatsioon (5)
- Frontend: komponentide kompileerimine, standalone property, template puudu (4)
- Juurutamine: DATABASE_URL, PostgreSQL URL parser, port, Vercel SPA, Angular output (10)
- Muu: bugfixid, idempotentsus (3)

**V4 fixid (5 committid):**
- Juurutamine: Render blueprint süntaks, Dockerfile context, PostgreSQL URL parser, DB migration retry, Render API URL (5)
- Backend: **0**
- Frontend: **0**

---

## 3. Koodimaht

| Komponent | V1 | V4 | Muutus |
|-----------|-----|-----|--------|
| **Backend (C#)** | 2 990 rida | 3 455 rida | +16% |
| **Frontend (TS/HTML/CSS)** | 1 666 rida | 1 240 rida | **-26%** |
| **Testid** | 125 rida | 110 rida | -12% |
| **Kokku** | **4 781 rida** | **4 805 rida** | ~0% |

**Märkimisväärne:** Koodimaht on praktiliselt identne, kuid:
- V4 backend on veidi suurem (+465 rida) tänu football-data.org integratsioonile ja MatchWithPredictionDto-le
- V4 frontend on 26% väiksem (6 vs 12 komponenti), kuna Opus tegi kompaktsemaid arhitektuurivalikuid (inline templates, vähem teenuseid)

| Struktuur | V1 | V4 |
|-----------|-----|-----|
| API otspunktid | 30 | **33** |
| Frontend komponendid | 12 | **6** |
| Frontend teenused | 7 | **4** |
| Ühiktestid | ~125 rida | ~110 rida |

---

## 4. Blokeerijate võrdlus

| Kategooria | V1 | V4 | Muutus |
|------------|-----|-----|--------|
| **Backend loogika** | 3 | **0** | **-100%** |
| **Frontend/UX** | 3 | **0** | **-100%** |
| **Juurutamine** | 5 | **5** | 0% |
| **Kokku** | **11** | **5** | **-55%** |

### V1 blokeerijate saatus V4-s

| V1 blokeerija | V4 staatus | Ennetanud |
|---------------|------------|-----------|
| .NET SDK versioon | **ENNETATUD** | ERROR 1 |
| dotnet-ef versioon | **ENNETATUD** | ERROR 2 |
| EF migratsiooni probleemid | **ENNETATUD** | ERROR 3 |
| PostgreSQL port conflict | **ENNETATUD** | ERROR 5 |
| Tailwind v4 ühildumatus | **ENNETATUD** | ERROR 8 |
| Angular outputPath | **ENNETATUD** | ERROR 9 |
| Vercel SPA routing | **ENNETATUD** | ERROR 12 |
| CORS konfiguratioon | **ENNETATUD** | ERROR 13 |
| JSON serialization loop | **ENNETATUD** | ERROR 15 |
| PostgreSQL URL parser | **KORDUS** (uus variant) | ERROR 7 (osaliselt) |
| Render API URL | **KORDUS** | ERROR 20 (osaliselt) |

**V1 ennetamismäär V4-s: 9/11 (82%)**

---

## 5. Spetsifikatsioonide analüüs

### Failide klassifikatsioon

| Kategooria | Failide arv | Ridade arv | Kirjeldus |
|------------|-------------|------------|-----------|
| **Agentide raamistik** | 20 | 1 189 | 13 agenti, 4 workflow'd, 3 malli — taaskasutatavad projektide üleselt |
| **Hübriid** | 2 | 247 | ERROR-PREVENTION.md (20 reeglit), START-HERE.md — raamistiku metoodika, projekti-spetsiifilne sisu |
| **Projekti-spetsiifilised** | 4 | 1 365 | ARCHITECTURE, GAME-RULES, REQUIREMENTS, TECH-STACK — ainult selle projekti jaoks |
| **Kokku** | **26** | **2 801** | |

### Raamistiku vs projekti sisendi osakaal

```
Raamistik (20 faili, 1189 rida):  ████████████████████░░░░░░  42%
Hübriid   (2 faili,  247 rida):   ███░░░░░░░░░░░░░░░░░░░░░░   9%
Projekt   (4 faili, 1365 rida):   █████████████░░░░░░░░░░░░░  49%
```

**Oluline eristus:** Agentide raamistik (20 faili) on **projektiülene vara** — need failid on loodud 3 tsükli jooksul ja on kasutatavad ükskõik millise projekti jaoks. Projekti-spetsiifilised failid (4 faili) on sisend, mis kirjeldab konkreetset rakendust.

### ERROR-PREVENTION.md tõhusus

| | V1 → V4 |
|---|---|
| Reegleid kokku | 20 |
| Täielikult ennetatud | **17 (85%)** |
| Osaliselt ennetatud (uus variant kordus) | 3 (15%) |
| Täielikult kordus | **0 (0%)** |

---

## 6. Juurutamise blokeerijate analüüs

Juurutamine on ainus kategooria, mis ei paranenud (5 → 5). Detailne võrdlus:

| V1 juurutamise probleem | V4 juurutamise probleem | Sama? |
|------------------------|------------------------|-------|
| DATABASE_URL ei loeta | Render blueprint connectionURI süntaks | **Sarnane** (platvormi API) |
| PostgreSQL URL → Npgsql | PostgreSQL URL port 443 (https default) | **Sama klaster** (3. kord) |
| Vercel outputPath | Dockerfile Docker context path | **Erinev** (uus platvorm) |
| Vercel SPA routing | DB migration crash | **Erinev** |
| Angular standalone property | Render API URL erinev oodatust | **Sarnane** (ettearvamatu URL) |

**Järeldus:** Juurutamise blokeerijad ei ole samad vead, mis korduvad — need on **sama kategooria uued variandid**. Kolmanda osapoole platvormid (Render, Vercel) muudavad API-sid ja käitumist, mistõttu neid ei saa täielikult ennetada.

---

## 7. Kokkuvõte

### Kvantitatiivne kokkuvõte

| Mõõdik | V1 | V4 | Paranemine |
|--------|-----|-----|-----------|
| **Aktiivne arendusaeg** | ~15h 24min | ~2h 54min | **-81%** (~5.3x kiirem) |
| **Sessioonid** | 12 | 2 | -83% |
| **Committid** | 72 | 15 | **-79%** |
| **Fix committid** | 22 | 5 | **-77%** |
| **Feat osakaal** | 36% | 53% | +17pp |
| **Docs committid** | 18 | 0 | -100% |
| **Blokeerijad** | 11 | 5 | **-55%** |
| **Backend/frontend blokeerijad** | 6 | 0 | **-100%** |
| **V1 blokeerijate ennetamismäär** | — | 82% | — |
| **ERROR-PREVENTION tõhusus** | — | 85% | — |

### Kvalitatiivsed järeldused

1. **Arenduskiirus paranes 5.3x** — 15h 24min → 2h 54min. Kogu rakendus (backend, frontend, testid, juurutamine) valmis 2 sessioonis vs 12 sessiooni üle 27 päeva.

2. **Backend ja frontend blokeerijad on elimineeritud** — ERROR-PREVENTION.md 20 reeglit ennetasid 85% teadaolevatest probleemidest. Ühtegi uut backend ega frontend blokeerijat ei esinenud.

3. **Juurutamine on "viimane piir"** — 100% V4 blokeeerijatest olid juurutamise kategoorias. Need on platvormi-spetsiifilised probleemid, mida spetsifikatsioonid ei suuda täielikult ennetada.

4. **PostgreSQL URL parser on persistentne probleemiklaster** — kordub V2, V3 ja V4-s erinevate variantidena. See on kandidaat raamistiku-taseme lahenduseks (nt. NuGet pakett, mis käsitleb kõiki variante).

5. **Mudeli mõju vs spetsifikatsioonide mõju** — Opus 4.6 tegi kompaktsemaid valikuid (6 vs 12 komponenti), kuid spetsifikatsioonid ennetasid konkreetseid teadaolevaid vigu (Tailwind v4, .NET SDK pin, PostgreSQL port), mida mudel üksi ei oleks teadnud. **Need kaks faktorit on komplementaarsed.**

6. **Dokumentatsiooni committid kadusid** — V1-s kulus 25% committeidest dokumentatsioonile (analüüsid, workflow'd). V4-s oli spetsifikatsioon juba valmis ja ühtegi docs committid ei olnud vaja. See näitab, et itereeritud spetsifikatsioonid elimineerivad jooksvat dokumenteerimisvajadust.

### Eksperimendi tulemus

Sama projekti sisendi ja itereeritud agentide raamistikuga saavutati:
- **5.3x kiirem arendus**
- **77% vähem vigade parandusi**
- **55% vähem blokeerijaid**
- **Kogu backend/frontend vigadeta**
- **Ainult platvormi-spetsiifilised juurutamisprobleemid jäid**

See kinnitab hüpoteesi, et **spetsifikatsioonide itereerimine muudab AI agendi oluliselt iseseisvamaks** — kuid näitab ka ennetamise ülempiiri, kus kolmandate osapoolte platvormide ettearvamatu käitumine jääb püsivaks väljakutseks.
