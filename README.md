🚀 שרת API לרשימות קניות
תיאור הפרויקט
שרת API מתקדם לניהול רשימות קניות הבנוי על .NET 8 עם Entity Framework Core. השרת מספק API מלא לניהול קטגוריות, פריטים ורשימות קניות עם תמיכה בפעולות CRUD מלאות.

תכונות עיקריות
🗂️ ניהול קטגוריות
יצירת קטגוריות חדשות - הוספה של קטגוריות מותאמות אישית
עדכון קטגוריות - עריכת שמות קטגוריות קיימות
מחיקת קטגוריות - הסרה בטוחה של קטגוריות לא בשימוש
שליפת כל הקטגוריות - קבלת רשימה מלאה של קטגוריות זמינות
📝 ניהול רשימות קניות
יצירת רשימות חדשות - הוספת רשימות עם שם לקוח ופריטים
עדכון רשימות - שינוי פרטי רשימה וסטטוס השלמה
מחיקת רשימות - הסרה מלאה של רשימות כולל פריטים
שליפת רשימות - קבלת רשימות עם כל הפריטים הקשורים
🛍️ ניהול פריטים
הוספת פריטים לרשימה - יצירת פריטים חדשים עם כמות וקטגוריה
עדכון פריטים - שינוי שם, כמות או קטגוריה של פריטים
מחיקת פריטים - הסרת פריטים מרשימות
שליפת פריטים - קבלת כל הפריטים של רשימה ספציפית
טכנולוגיות בשימוש
⚙️ Backend
.NET 8 - פלטפורמת פיתוח מתקדמת ומהירה
ASP.NET Core Web API - מסגרת לבניית API מודרני
Entity Framework Core - ORM מתקדם לניהול בסיס נתונים
🔧 כלי פיתו
Docker - קונטיינריזציה לפריסה קלה
Swagger/OpenAPI - תיעוד API אוטומטי
CORS - תמיכה בבקשות מדומיינים שונים
מבנה הפרויקט
ShoppingListAPI/
├── Controllers/          # בקרי API
├── Models/              # מודלי נתונים
├── Data/                # הקשר לבסיס נתונים
├── DTOs/                # אובייקטי העברת נתונים
├── Services/            # שירותי עסק
├── Program.cs           # נקודת כניסה ראשית
└── Dockerfile          # הגדרות Docker

Copy

Apply

API Endpoints
📂 קטגוריות
GET    /api/categories           # שליפת כל הקטגוריות
GET    /api/categories/{id}      # שליפת קטגוריה ספציפית
POST   /api/categories           # יצירת קטגוריה חדשה
PUT    /api/categories/{id}      # עדכון קטגוריה
DELETE /api/categories/{id}      # מחיקת קטגוריה

Copy

Apply

📋 רשימות קניות
GET    /api/shoppinglists        # שליפת כל הרשימות
GET    /api/shoppinglists/{id}   # שליפת רשימה ספציפית
POST   /api/shoppinglists        # יצירת רשימה חדשה
PUT    /api/shoppinglists/{id}   # עדכון רשימה
DELETE /api/shoppinglists/{id}   # מחיקת רשימה

Copy

Apply

🛍️ פריטים
GET    /api/shoppinglists/{id}/items     # שליפת פריטי רשימה
POST   /api/shoppinglists/{id}/items     # הוספת פריט לרשימה
PUT    /api/shoppinglists/{listId}/items/{itemId}    # עדכון פריט
DELETE /api/shoppinglists/{listId}/items/{itemId}    # מחיקת פריט

Copy

Apply

התקנה והפעלה
📋 דרישות מערכת
.NET 8 SDK
SQL Server (LocalDB או מלא)
Docker (אופציונלי)
⚙️ הפעלה רגילה
# שכפול הפרויקט
git clone [repository-url]

# מעבר לתיקיית השרת
cd ShoppingListAPI

# שחזור חבילות NuGet
dotnet restore

# עדכון בסיס נתונים
dotnet ef database update

# הפעלת השרת
dotnet run

Copy

Execute

🐳 הפעלה עם Docker
# בניית תמונת Docker
docker build -t shopping-list-api .

# הפעלת קונטיינר
docker run -p 5000:80 shopping-list-api

Copy

Execute

📊 מודל נתונים
Categories - קטגוריות פריטים
ShoppingLists - רשימות קניות עם פרטי לקוח
ShoppingItems - פריטים ברשימות עם כמויות
טיפול בשגיאות
השרת כולל מערכת מקיפה לטיפול בשגיאות:

Try-Catch בכל הבקרים - לכידה ומעבד של חריגות
הודעות שגיאה ברורות - החזרת הודעות מובנות למשתמש
קודי סטטוס HTTP נכונים - 200, 201, 400, 404, 500
לוגים מפורטים - רישום שגיאות לצורך דיבוג
אבטחה ו-CORS
הגדרות CORS - תמיכה בבקשות מהלקוח
ולידציה של נתונים - בדיקת תקינות נתונים נכנסים
הגנה מפני SQL Injection - שימוש ב-Entity Framework
טיפול בנתונים רגישים - הצפנה והגנה על מידע
תיעוד API
השרת כולל תיעוד אוטומטי עם Swagger:

ממשק גרפי - בדיקת API דרך הדפדפן
תיעוד מפורט - תיאור כל endpoint ופרמטר
דוגמאות בקשות - קוד לדוגמה לכל פעולה
גישה לתיעוד: http://localhost:5000/swagger

פריסה לייצור
🌐 הגדרות ייצור
משתני סביבה - הגדרת מחרוזות חיבור
לוגים מתקדמים - רישום פעילות מפורט
אופטימיזציה - הגדרות ביצועים מיטביות
בטיחות - הגדרות אבטחה מחמירות
יצירת קשר
👩‍💻 מפתחת: ATARA TACH
📞 טלפון: 058-328-0005
📧 אימייל: 0583280005a@gmail.com

© כל הזכויות שמורות 2024 ATARA TACH

שרת API מתקדם לרשימות קניות - פותח ב-.NET 8 עם Entity Framework Core
