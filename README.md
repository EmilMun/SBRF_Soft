## SBRF

Небольшая утилита для работы с банковским терминалом (насколько мне известно, система от Сбербанка).

Утилита специализированна под конкретную программу. Это просто графическая оболочка для работой с оной.

Как работает терминал, программа, мне неизвестно.

Известно только три команды, которые принимает (***loadparm.exe*** - в репозиторий не входит).
```cmd
loadparm.exe 1 100
:: Это будет оплата на 1 руб
loadparm.exe 3 100
:: Это будет возврат клиенту 1 руб
```
