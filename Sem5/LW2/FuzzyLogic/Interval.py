# Практическая работа №2 по дисциплине ЛОИС
# Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
# Выполнили студенты группы 221701 БГУИР:
# - Робилко Тимур Маркович
# - Абушкевич Алексей Александрович
#
# Файл, содержащий класс, который отвечает за представление интервала
# 28.11.2024
#
# Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.

from __future__ import annotations

from FuzzyLogic.FuzzyValue import FuzzyValue


class Interval:
    def __init__(self, bottom: FuzzyValue, top: FuzzyValue) -> None:
        if top < bottom:
            top, bottom = bottom, top

        self.bottom: FuzzyValue = bottom
        self.top: FuzzyValue = top

    def __contains__(self, item: FuzzyValue) -> bool:
        return self.bottom <= item <= self.top

    def __mul__(self, other: Interval | None):
        if other is None:
            return None

        if self.bottom <= other.top and other.bottom <= self.top:
            return Interval(max(self.bottom, other.bottom),
                            min(self.top, other.top))
        else:
            return None

    def __str__(self) -> str:
        if self.bottom.value == self.top.value:
            return "{" + str(self.bottom) + "}"
        else:
            return "[" + str(self.bottom) + ";" + str(self.top) + "]"

    def __eq__(self, other: Interval) -> bool:
        return self.bottom == other.bottom and self.top == other.top
