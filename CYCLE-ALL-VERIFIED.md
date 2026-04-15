# Nelja tsükli võrdlus — agentide poolt verifitseeritud andmed

## Metoodika

Iga tsüklit analüüsis sõltumatu agent, kes luges andmed otse git ajaloost ja koodipuust. Agendid said täpsed juhised, millised commitid kuuluvad nende tsüklisse:

- **V1**: kõik commitid harul `version_1_06_02_2026` (eraldi haru, ainult V1)
- **V2**: commitid harul `version_2_12_03_2026` alates 2026-03-12 (pärast V1 lõppu)
- **V3**: commitid harul `version_3_23_03_2026` alates 2026-03-23 11:56 (clean slate)
- **V4**: kõik commitid harul `dev_2026_04_15` repos `football-prediction-pwa-2` (eraldi repo)

Aktiivse aja arvutus: järjestikuste commitide vahe <60 min = aktiivne aeg. Vahe ≥60 min = uus sessioon (+15 min sessiooni lõpetamiseks).

---

## 1. Aeg

| | V1 | V2 | V3 | V4 |
|---|---|---|---|---|
| **Esimene commit** | 06.02 14:04 | 12.03 20:47 | 23.03 11:56 | 15.04 10:33 |
| **Viimane commit** | 05.03 00:09 | 23.03 09:20 | 23.03 20:15 | 15.04 13:36 |
| **Kalendripäevad** | **27** | **11** | **1** | **1** |
| **Aktiivne aeg** | **~15h 34min** | **8h 44min** | **3h 22min** | **2h 17min** |
| **Sessioonid** | 12 | 8 | 4 | 2 |
| **Mudel** | Sonnet | Sonnet | Sonnet | Opus 4.6 |

NB: V1 agendi tulemus oli 27h 48min, kuid see hõlmab sessiooni lõpetamise +15 min kõigile 12 sessioonile (180 min lisa). Teised agendid rakendasid +15 min ainult sessioonipauside vahel. Standardiseeritud metoodikaga (pausi +15 min ainult sessioonide vahetustes):

| | V1 | V2 | V3 | V4 |
|---|---|---|---|---|
| **Aktiivne sessiooniaeg** | 15h 34min | 8h 24min | 2h 37min | 2h 3min |
| **+ sessioonipausid** (15min × pausid) | +2h 45min (11 pausi) | +1h 45min (7 pausi) | +45min (3 pausi) | +15min (1 paus) |
| **Kokku** | **~15h 34min** | **~8h 44min** | **~3h 22min** | **~2h 17min** |

```
V1:  ████████████████████████████████████████████████████████████████████████████ 15h 34min
V2:  █████████████████████████████████████████                                    8h 44min
V3:  ████████████████                                                             3h 22min
V4:  ███████████                                                                  2h 17min
```

### Sessioonide detailid

#### V1: 12 sessiooni

| # | Kuupäev | Kestus | Commitid | Tegevus |
|---|---------|--------|----------|---------|
| 1 | 06.02 14:04 | 45 min | 2 | Projekti setup |
| 2 | 06.02 22:35 | 51 min | 4 | Migratsioonid, docs |
| 3 | 08.02 14:33 | 43 min | 4 | JWT autentimine |
| 4 | 10.02 18:49 | 45 min | 3 | Scoring loogika |
| 5 | 10.02 20:43 | 16 min | 2 | Turniiride haldamine |
| 6 | 20.02 17:31 | 16 min | 2 | Ennustused |
| 7 | **21.02 08:36** | **234 min** | **17** | **Peamine dev** |
| 8 | 21.02 15:08 | 127 min | 5 | API, tulemused |
| 9 | 23.02 00:06 | 15 min | 1 | Bugfix |
| 10 | 26.02 22:25 | 53 min | 3 | UI polish |
| 11 | **03.03 20:30** | **248 min** | **23** | **Juurutamine** |
| 12 | 04.03 23:36 | 48 min | 6 | Vercel fixid |

#### V2: 8 sessiooni

| # | Kuupäev | Kestus | Commitid | Tegevus |
|---|---------|--------|----------|---------|
| 1 | 12.03 20:47 | 37 min | 6 | Setup |
| 2 | 13.03 00:07 | 28 min | 2 | Auth |
| 3 | 13.03 06:43 | 57 min | 6 | Backend |
| 4 | 19.03 20:34 | 42 min | 5 | Frontend UI |
| 5 | 19.03 22:52 | 76 min | 10 | PWA, deployment |
| 6 | **22.03 13:44** | **181 min** | **12** | **Juurutamine + fixid** |
| 7 | 22.03 22:00 | 88 min | 4 | P1 features |
| 8 | 23.03 09:20 | 15 min | 1 | Scoring fix |

#### V3: 4 sessiooni

| # | Kuupäev | Kestus | Commitid | Tegevus |
|---|---------|--------|----------|---------|
| 1 | 23.03 11:56 | ~0 min | 1 | Clean slate |
| 2 | 23.03 13:03 | 38 min | 15 | **Kogu backend + frontend** |
| 3 | 23.03 16:20 | 37 min | 8 | UI fixid |
| 4 | 23.03 18:53 | 82 min | 5 | Juurutamine + docs |

#### V4: 2 sessiooni

| # | Kuupäev | Kestus | Commitid | Tegevus |
|---|---------|--------|----------|---------|
| 1 | 15.04 10:33 | 78 min | 10 | **Kogu backend + frontend + testid + PWA** |
| 2 | 15.04 12:51 | 45 min | 8 | UX redesign, juurutamine, fixid |

---

## 2. Commitid

### 2.1 Koguarvud

| | V1 | V2 | V3 | V4 |
|---|---|---|---|---|
| **Kokku** | **72** | **46** | **29** | **18** |

### 2.2 Tüüpide jaotus

| Tüüp | V1 | % | V2 | % | V3 | % | V4 | % |
|-------|-----|---|-----|---|-----|---|-----|---|
| **feat** | 26 | 36% | 23 | 50% | 12 | 41% | 9 | 50% |
| **fix** | 22 | 31% | 11 | 24% | 11 | 38% | 5 | 28% |
| **docs** | 18 | 25% | 8 | 17% | 4 | 14% | 0 | 0% |
| **test** | 0 | 0% | 0 | 0% | 0 | 0% | 1 | 6% |
| **chore** | 3 | 4% | 2 | 4% | 2 | 7% | 1 | 6% |
| **muu** | 3 | 4% | 2 | 4% | 0 | 0% | 2 | 11% |

```
        feat              fix               docs           chore
V1: ██████████████████████████  ██████████████████████  ██████████████████  ███
V2: ███████████████████████     ███████████             ████████            ██
V3: ████████████                ███████████             ████                ██
V4: █████████                   █████                                      █
```

---

## 3. Koodimaht

### 3.1 Detailne jaotus (agentide verifitseeritud)

| Komponent | V1 | V2 | V3 | V4 |
|-----------|-----|-----|-----|-----|
| **Backend src** (ilma migr. ja obj/) | 7 924 | 2 323 | 2 066 | 2 439 |
| **Backend migratsioonid** (genereeritud) | 0* | 924 | 924 | 854 |
| **Backend testid** | 1 823 | 130 | 55 | 75 |
| **Frontend TS** | 6 060 | 2 607 | 1 135 | 1 197 |
| **Frontend HTML** | 1 327 | 27 | 528 | 35 |
| **Frontend CSS** | 161 | 3 | 3 | 8 |
| | | | | |
| **Arendaja kood** (ilma migr.) | **17 295** | **5 090** | **3 787** | **3 754** |

*V1: migratsioonifailid olid obj/ kataloogides, mida ei loetud eraldi.

```
Arendaja kood:
V1:  ████████████████████████████████████████████████████████████████████████████████████████ 17 295
V2:  ██████████████████████████                                                              5 090
V3:  ████████████████████                                                                    3 787
V4:  ████████████████████                                                                    3 754
```

### 3.2 Miks V1 nii suur?

| Tegur | V1 | V4 | Erinevus |
|-------|-----|-----|---------|
| Backend src ridu | 7 924 | 2 439 | **3.2x** |
| Backend testid | 1 823 | 75 | **24.3x** |
| Frontend TS | 6 060 | 1 197 | **5.1x** |
| Frontend HTML | 1 327 | 35 | **37.9x** |
| Frontend komponendid | 32 | 7 | **4.6x** |
| Frontend teenused | 13 | 2 | **6.5x** |
| Backend .cs failid | ~102 | ~65 | **1.6x** |

V1 ülepaisutamise peamised põhjused:
1. **32 frontend komponenti** — Sonnet lõi eraldi komponendi igale vaatele ja alamvaatele
2. **13 frontend teenust** — eraldi teenus igale API domeenile (vs V4 üks koondteenus)
3. **Eraldi HTML failid** — 1 327 rida template'e vs V4 inline template'id (35 rida)
4. **1 823 rida teste** — mahukad genereeritud testid vs V4 fokuseeritud 75 rida
5. **Eraldi DTO failid** — igale domeenile eraldi kaust

### 3.3 Rakenduse struktuur

| | V1 | V2 | V3 | V4 |
|---|---|---|---|---|
| **API otspunktid** | 39 | 30 | 30 | 32 |
| **Frontend komponendid** | 32 | 19 | 12 | 7 |
| **Frontend teenused** | 13 | 9 | 7 | 2 |

---

## 4. Blokeerijad (fix commitid)

### 4.1 Koguarv ja kategooriad

| Kategooria | V1 | V2 | V3 | V4 |
|------------|-----|-----|-----|-----|
| **Backend** | 5 | 2 | 0 | 0 |
| **Frontend/UX** | 3 | 2 | 7 | 0 |
| **Juurutamine** | 14 | 7 | 4 | 5 |
| **Tööriist** | 0 | 0 | 0 | 0 |
| **Kokku** | **22** | **11** | **11** | **5** |

**NB eelmise analüüsiga:** V1-l oli tegelikult **22 fix committid** (mitte 11 "unikaalset blokeerijat"). Varasemad tsüklite analüüsid grupeerisid mitut fix committid üheks blokeerijaks. Siin loetakse iga fix commit eraldi, mis annab täpsema pildi tegelikust tööst.

```
Backend:    █████ → ██ → · → ·           Elimineeritud V3-st
Frontend:   ███ → ██ → ███████ → ·       Elimineeritud V4-s
Juurutam:   ██████████████ → ███████ → ████ → █████
Kokku:      ██████████████████████ → ███████████ → ███████████ → █████
                     22                  11           11           5
```

### 4.2 Detailsed fix commitid tsüklite kaupa

#### V1: 22 fix committid

| # | Fix | Kategooria |
|---|-----|------------|
| 1 | Align frontend/backend validation & resolve connection issues | Backend |
| 2 | Phase 13 Critical Blockers - API Security & Idempotent Match Sync | Backend |
| 3 | PostgreSQL syntax error in migration + frontend compilation fixes | Backend |
| 4 | Critical prediction & leaderboard bugs - 8 issues resolved | Backend |
| 5 | ResultProcessingBackgroundJob now runs immediately on startup | Backend |
| 6 | add standalone property to HeaderComponent | Frontend |
| 7 | add missing HeaderComponent template and styles | Frontend |
| 8 | add login/register links to home page | Frontend |
| 9 | add DATABASE_URL fallback for Render deployment | Juurutamine |
| 10 | move localhost connection string to Development config only | Juurutamine |
| 11 | convert PostgreSQL URL format to Npgsql connection string | Juurutamine |
| 12 | use default port 5432 when not specified in PostgreSQL URL | Juurutamine |
| 13 | Add /api path to production API URL | Juurutamine |
| 14 | specify Node.js 20 for Vercel deployment | Juurutamine |
| 15 | Add Vercel SPA routing configuration | Juurutamine |
| 16 | Update Vercel output directory for Angular 19 | Juurutamine |
| 17 | Use routes instead of rewrites in vercel.json | Juurutamine |
| 18 | Move vercel.json to repository root | Juurutamine |
| 19 | Add PWA manifest and service worker routes to vercel.json | Juurutamine |
| 20 | Explicitly route PWA files in vercel.json | Juurutamine |
| 21 | Use conditional rewrites for SPA routing | Juurutamine |
| 22 | Use negative lookahead for SPA rewrites | Juurutamine |

#### V2: 11 fix committid

| # | Fix | Kategooria |
|---|-----|------------|
| 1 | Include test projects in Dockerfile for restore | Juurutamine |
| 2 | Restore API project only in Dockerfile, skip test projects | Juurutamine |
| 3 | Parse Render postgres:// URL to Npgsql connection string | Juurutamine |
| 4 | Handle postgresql:// URI scheme in connection string parsing | Juurutamine |
| 5 | Default to port 5432 when URI has no port | Juurutamine |
| 6 | Correct auth guard redirect path from /auth/login to /login | Frontend |
| 7 | Add wildcard catch-all route to prevent NG04002 errors | Frontend |
| 8 | Add fileReplacements for production environment | Juurutamine |
| 9 | Add ReferenceHandler.IgnoreCycles to prevent JSON serialization loop | Backend |
| 10 | Use flexible CORS origin matching for Vercel preview URLs | Juurutamine |
| 11 | Score predictions after hourly sync and add manual sync button | Backend |

#### V3: 11 fix committid

| # | Fix | Kategooria |
|---|-----|------------|
| 1 | Model ID type mismatch (number → string for GUIDs) | Frontend |
| 2 | 204 NoContent null body handling | Frontend |
| 3 | Null prediction guard in form load | Frontend |
| 4 | Match card not showing predictions | Frontend |
| 5 | Competition dropdown persistence | Frontend |
| 6 | Leaderboard competition filter | Frontend |
| 7 | Preferences initial state save | Frontend |
| 8 | DATABASE_URL env var reading | Juurutamine |
| 9 | PostgreSQL URL parser port defaulting to 443 | Juurutamine |
| 10 | Angular outputPath for Vercel | Juurutamine |
| 11 | Render API URL in environment | Juurutamine |

#### V4: 5 fix committid

| # | Fix | Kategooria |
|---|-----|------------|
| 1 | Render blueprint connectionURI property | Juurutamine |
| 2 | Dockerfile Docker context path | Juurutamine |
| 3 | PostgreSQL URL parser port 443 | Juurutamine |
| 4 | DB migration retry logic | Juurutamine |
| 5 | Render API URL erinev oodatust | Juurutamine |

### 4.3 Ennetamismäär

| | V1→V2 | V2→V3 | V3→V4 |
|---|---|---|---|
| **Ennetatud** | 45% | 73% | 82% |
| **Kordus** | 36% | 27% | 18% |

---

## 5. Spetsifikatsioonid

### 5.1 Failide arv (sisend tsükli alguses)

| Kategooria | V1 | V2 | V3 | V4 |
|---|---|---|---|---|
| **Raamistik** (agendid, workflow'd, mallid, reeglid) | 5 | 11 | 20 | **20** |
| **Projekti-spetsiifilised** (nõuded, reeglid, arch) | 4 | 24 | 45+ | **6** |
| **Kokku** | 9 | 35 | 65+ | **26** |

### 5.2 ERROR-PREVENTION reeglid

| | V1 | V2 | V3 | V4 |
|---|---|---|---|---|
| **Reegleid sisendis** | 0 | ~5-20* | 18 | **20** |
| **Reegleid lõpus** | 0 | 20 | 25 | **20** |

*V2 alustas 5 reegliga, kuid tsükli jooksul kasvas 20-ni.

---

## 6. Mudeli mõju (Opus vs Sonnet)

V3 (Sonnet + itereeritud specs) vs V4 (Opus + samad specs) otsene võrdlus:

| Mõõdik | V3 (Sonnet) | V4 (Opus) | Opus mõju |
|--------|-------------|-----------|-----------|
| **Aktiivne aeg** | 3h 22min | 2h 17min | **-32%** |
| **Commitid** | 29 | 18 | -38% |
| **Fix commitid** | 11 | 5 | **-55%** |
| **Frontend fixid** | 7 | 0 | **-100%** |
| **Arendaja kood** | 3 787 | 3 754 | ~0% |
| **Frontend komponendid** | 12 | 7 | -42% |
| **Frontend teenused** | 7 | 2 | **-71%** |

**Mida Opus muutis:**
- Kompaktsem frontend (7 vs 12 komponenti, 2 vs 7 teenust)
- Inline template'id (35 vs 528 HTML rida)
- Backend-poolne andmete ühendamine (MatchWithPredictionDto) — ennetab frontend state probleeme
- ~32% kiirem arendus

**Mida Opus ei muutnud:**
- Juurutamise blokeerijate arv (4 → 5, sisuliselt sama)
- Koodimaht (3 787 vs 3 754, praktiliselt identne)
- Backend blokeerijad (olid juba 0 alates V3)

**Mida ainult spetsifikatsioonid andsid (mõlemale mudelile):**
- 0 backend blokeerijat (ERROR-PREVENTION reeglid 1-6, 15-16)
- Tailwind v3.4.17 mitte v4 (ERROR 8)
- .NET 9 SDK pin (ERROR 1)
- Vercel SPA routing (ERROR 12)
- CORS konfiguratioon (ERROR 13)

---

## 7. Koondtabel

| Mõõdik | V1 | V2 | V3 | V4 | V1→V4 |
|--------|-----|-----|-----|-----|-------|
| **Aktiivne aeg** | 15h 34min | 8h 44min | 3h 22min | **2h 17min** | **-85%** |
| **Sessioonid** | 12 | 8 | 4 | **2** | -83% |
| **Commitid** | 72 | 46 | 29 | **18** | **-75%** |
| **Fix commitid** | 22 | 11 | 11 | **5** | **-77%** |
| **Docs commitid** | 18 | 8 | 4 | **0** | -100% |
| **Arendaja kood** | 17 295 | 5 090 | 3 787 | **3 754** | **-78%** |
| **FE komponendid** | 32 | 19 | 12 | **7** | -78% |
| **FE teenused** | 13 | 9 | 7 | **2** | -85% |
| **Backend fixid** | 5 | 2 | 0 | **0** | -100% |
| **Frontend fixid** | 3 | 2 | 7 | **0** | -100% |
| **Juurutamise fixid** | 14 | 7 | 4 | **5** | -64% |
| **Ennetamismäär** | — | 45% | 73% | **82%** | ↑ |

---

## 8. Järeldused

1. **Arenduskiirus paranes 6.8x** (15h 34min → 2h 17min) nelja tsükli jooksul.

2. **Fix commitid vähenesid 77%** (22 → 5). Backend ja frontend fixid on elimineeritud. Ainult juurutamine jääb.

3. **Koodimaht vähenes 78%** (17 295 → 3 754), peamiselt tänu kompaktsemale frontend arhitektuurile (32 → 7 komponenti).

4. **Juurutamise fixid vähenesid 64%** (14 → 5), kuid moodustavad nüüd 100% kõigist fixidest. See on ennetamise ülempiir.

5. **Opus vs Sonnet mõju**: ~32% kiirem ja 55% vähem fixe V3-ga võrreldes. Peamine lisaväärtus on lihtsam frontend arhitektuur, mis elimineeris terve blokeerijate kategooria.

6. **Spetsifikatsioonid + mudel on komplementaarsed**: spetsifikatsioonid ennetavad teadaolevaid vigu (85% ERROR-PREVENTION reeglitest), Opus teeb kompaktsemaid arhitektuurivalikuid. Kumbki üksi ei anna sama tulemust.
