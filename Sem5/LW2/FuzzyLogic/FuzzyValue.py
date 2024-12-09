# Практическая работа №2 по дисциплине ЛОИС
# Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
# Выполнили студенты группы 221701 БГУИР:
# - Робилко Тимур Маркович
# - Абушкевич Алексей Александрович
#
# Классы, отвечающие за представление результата решения нечеткого уравнения
# 28.11.2024
#
# Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.

from __future__ import annotations


class FuzzyValue:
    def __init__(self, value: float) -> None:
        self.value: float = max(0.0, min(1.0, round(value, 8)))

    def __hash__(self) -> int:
        return hash(self.value)

    def __str__(self) -> str:
        return str(self.value)

    def __eq__(self, other) -> bool:
        return self.value == other.value

    def __lt__(self, other: FuzzyValue) -> bool:
        return self.value < other.value

    def __le__(self, other: FuzzyValue) -> bool:
        return self.value <= other.value

    def __ge__(self, other: FuzzyValue) -> bool:
        return self.value >= other.value
