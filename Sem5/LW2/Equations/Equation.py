# Практическая работа №2 по дисциплине ЛОИС
#  Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
#  Выполнили студенты группы 221701 БГУИР:
#  - Робилко Тимур Маркович
#  - Абушкевич Алексей Александрович
#
#  Классы, отвечающие за представление нечеткого уравнение
#  28.11.2024
#
#  Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.


from FuzzyLogic.FuzzyValue import FuzzyValue
from FuzzyLogic.FuzzyInterval import FuzzyInterval
from Equations.Answer import Answer
from FuzzyLogic.functions import invalid_type_error


def equality(x: float, y: float, name: str):
    if y == 0.0:
        return Answer({name: FuzzyInterval(FuzzyValue(0.0), FuzzyValue(1.0 - x))})
    elif y > x:
        return Answer()
    else:
        return Answer({name: FuzzyInterval(FuzzyValue(y - x + 1.0), FuzzyValue(y - x + 1.0))})


def less_equal(x: float, y: float, name: str):
    return Answer({name: FuzzyInterval(FuzzyValue(0.0), FuzzyValue(y - x + 1.0))})


def greater_equal():
    return


operation = {
    "==": equality,
    "<=": less_equal,
    ">=": greater_equal
}


class Equation:
    def __init__(self, x_name: str, value_x: FuzzyValue, value_y: FuzzyValue, binary_operator) -> None:
        if isinstance(x_name, str):
            self.x_name: str = x_name
        else:
            invalid_type_error(self.__init__, x_name, str)

        if isinstance(value_x, FuzzyValue):
            self.value_x: FuzzyValue = value_x
        elif isinstance(value_x, float):
            self.value_x: FuzzyValue = FuzzyValue(value_x)
        else:
            invalid_type_error(self.__init__, value_x, FuzzyValue)

        if isinstance(value_y, FuzzyValue):
            self.value_y: FuzzyValue = value_y
        elif isinstance(value_y, float):
            self.value_y: FuzzyValue = FuzzyValue(value_y)
        else:
            invalid_type_error(self.__init__, value_y, FuzzyValue)

        self.binary_operator = binary_operator
        self.keys: set = set()
        self.keys.add(x_name)

    def calculate_answers(self) -> Answer:
        return self.binary_operator(self.value_x.value, self.value_y.value, self.x_name)

    def __str__(self) -> str:
        op = str()
        if self.binary_operator == equality:
            op = "=="
        elif self.binary_operator == less_equal:
            op = "<="
        return f"max(0, {self.x_name} + {self.value_x} - 1) {op} {self.value_y}"
