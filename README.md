# DoomerNovel
Первая визуальная новела с использованием Unity и Ink

Целью является создать первый достаточно сложный проект, чтобы проявить свои силы и навыки в дизайне и проектировании.

В основе лежит Ink – инструмент для написания и запуска текстовых квестов. Загрузкой и хранением данных квеста занимаются отдельные сущности, которые могут быть заменены, если будет использоваться другой инструмент.

В центре всего взаимодействия лежит своеобразный Presenter в моей MVP, который выступает Фасадом-Посредником для взаимодействия логики текстового квеста (далее диалога), сценой диалога, командами изменения различных состояний диалога и менеджером звуков.

Для преобразования данных диалога в соответствующие изменения сцены диалога Presenter через Фабрику (является ScriptableObject) создает объекты-команды, которые через Presenter совершают соответствющие изменения.
Такие команды можно легко добавить по ходу разработки.

Элементы сцены диалога объеденины Компоновщиком в древовидную структуры. Через выделенный корень Presenter может получать элементы по Id, удалять элементы и т.д.
Новые элементы могут легко добавляться в проект по ходу разработки. Для ускорения работы, используется специальный кеш, который сохраняет результат обхода дочерних элементов в родительском.
Для создания элементов используется Фабрика, которая занимается инстанцированием шаблонов элементов.

Некоторые действия игрока могут вызывать события на элементах, которые могут передаваться по Цепочке обязанностей вверх по иерархии, пока не будут обработаны одним из родителей.
Основным обработчиком событий является корневой элемент. При этом обработка события происходит через вызов делегата, который связан с определенным объектом, на который нужно спроецировать запрос, что убирает необходимость у элементов знать о других объектах.

Корневой элемент делегирует обработку событий и обновление на каждом фрейме своим Состояниям. Пока у диалога выделено как минимум 4 Состояния: состояние бездействия, реагирование на нажатие для продвижения вперед, ускоренный режим и режим выбора.
Все Состояния являются ScriptableObject и могут сменять друг друга через вызовы отдельных методов у корня.

Пока проект находится в разработке. Отдельные детали будут добавлены со временем.
