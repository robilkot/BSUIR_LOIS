# Практическая работа №2 по дисциплине ЛОИС
#  Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
#  Выполнили студенты группы 221701 БГУИР:
#  - Робилко Тимур Маркович
#  - Абушкевич Алексей Александрович
#
#  Классы, отвечающие за представление степени истинности
#  28.11.2024
#
#  Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.


from .functions import invalid_type_error


class FuzzyValue:
    def __init__(self, value: float) -> None:
        if isinstance(value, float):
            self.value: float = max(0.0, min(1.0, round(value, 8)))
        elif isinstance(value, FuzzyValue):
            self.value: float = value.value
        else:
            invalid_type_error(self.__init__, value, float)

    def __str__(self) -> str:
        return str(self.value)

    def __lt__(self, other) -> bool:
        if isinstance(other, FuzzyValue):
            return self.value < other.value
        elif isinstance(other, float):
            return self.value < other
        else:
            invalid_type_error(self.__lt__, other, FuzzyValue)

    def __le__(self, other) -> bool:
        if isinstance(other, FuzzyValue):
            return self.value <= other.value
        elif isinstance(other, float):
            return self.value <= other
        else:
            invalid_type_error(self.__le__, other, FuzzyValue)

    def __ge__(self, other) -> bool:
        if isinstance(other, FuzzyValue):
            return self.value >= other.value
        elif isinstance(other, float):
            return self.value >= other
        else:
            invalid_type_error(self.__ge__, other, FuzzyValue)
