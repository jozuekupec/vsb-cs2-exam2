# Cvičení 2 - Reflexe

- Stáhněte si předpřipravenou aplikaci Reflection.zip. Knihovnu tříd NEPŘIDÁVEJTE do referencí konzolové aplikace.
- V rámci konzolové aplikace načtěte knihovnu tříd, která je součásti stažené aplikace (pomocí refelexe). Následně vytvořte instanci třídy CustomerController a zavolejte metodu List (vše pomocí reflexe). Výsledek volání metody List vypište do konzole.
- Rozšiřte deklaraci metody List o parametr "limit" (int), který bude udávat kolik zákazníků se má po zavolání této metody vrátit. Takto upravenou metodu opět zavolejte pomocí reflexe.
- V konzolové aplikaci vytvořte proměnnou "url" typu string s touto hodnotou: "/Customer/List?limit=2".
- V konzolové aplikaci upravte/doplňte váš kód tak aby se hodnota proměnné url mapovala na konkrétní kód třídu, metodu a parametry této metody. A to na základě těchto podmínek:
  - Text za prvním lomítkem je vždy prefix názvu třídy (například: "XyzController").
  - Text za druhým lomítkem je vždy název metody v rámci třídy.
  - Za otazníkem následují parametry volání metody. Ty jsou vždy ve formátu: "nazev=hodnota&nazev2=hodnota&nazev3=....."
- Váš kód musí splňovat následující:
  - Bude kontrolována existence požadované třídy a metody. Pokud neexistuje bude vypsán text "Stránka nenalezena".
  - Požadovaná metody bude zavolána pouze pokud je veřejná a má návratovou hodnotu typu string. V opačném případě bude vypsán text "Stránka nenalezena".
- Ve třídě CustomerController vytvořte metodu "Add" s parametry Name, Age a IsActive. V rámci metody dojde k vytvoření nového zákazníka a přidání do seznamu zákazníků. Metoda bude vracet string s Id nově přidaného zákazníka. Metodu otestujte pomocí url: "/Customer/Add?Name=Pepa&Age=30&IsActive=true".
- Upravte metodu Add tak aby měla jediný parametr, který bude typu Customer. Následně upravte váš kód v konzolové aplikaci tak aby v případě, že je parametrem metody něco, co vzniklo z třídy (vlastnost "IsClass" na Type je True) a zároveň nejde o string (typ != typeof(string)), tak dojde k namapování hodnot z URL na vlastnosti dané třídy. Jinými slovy, v případě, že je typ parametru metody něco, co vzniklo z třídy:
- Vytvořte instanci této třídy pomocí reflexe.
- Projděte všechny vlastnosti této instance a vyplňte je daty z URL.
- Při volání metody použijte právě vytvořenou instanci.
- Vytvořte vlastní atribut "Ignore". Tento atribut přidejte k vlastnosti IsActive třídy Customer. Následně upravte kód pro mapování hodnot z URL na objekt, tak aby v případě že má vlastnost atribut Ignore, nedošlo k mapování této vlastnosti.
- Vše řádně otestujte!

## Úloha k procvičení

```csharp
class MySerializer
{
    public static string Serialize(object value)
    {
        // TODO: doplnit - formát do kterého se bude serializovat si zvolte sami
    }

    public static T Deserialize<T>(string value)
    {
        // TODO: doplnit
    }
}


string txt = MySerializer.Serialize(new Customer() {
    // TODO: vymyslete si nějaké vlastnoosti + zkuste i jinou třídu
});
Customer obj = MySerializer.Deserialize<Customer>(txt);
```