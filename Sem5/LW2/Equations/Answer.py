# Практическая работа №2 по дисциплине ЛОИС
#  Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
#  Выполнили студенты группы 221701 БГУИР:
#  - Робилко Тимур Маркович
#  - Абушкевич Алексей Александрович
#
#  Классы, отвечающие за представление результата решения нечеткого уравнения
#  28.11.2024
#
#  Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.

from FuzzyLogic.functions import invalid_type_error


class Answer(dict):
    def __init__(self, intervals: dict = None, solutions: list = None, type_of_answer: str = None):
        super().__init__()
        if solutions is None:
            solutions = list()
            self.solutions: list = solutions
        elif isinstance(solutions, list):
            self.solutions: list = solutions
        else:
            invalid_type_error(self.__init__, solutions, list)

        if intervals is None:
            intervals = dict()
            for key, value in intervals.items():
                self[key] = value
        elif isinstance(intervals, dict):
            for key, value in intervals.items():
                self[key] = value
        else:
            invalid_type_error(self.__init__, intervals, dict)

        if type_of_answer is None:
            type_of_answer = "and"
        elif type_of_answer not in ("and", "or"):
            invalid_type_error(self.__init__, type_of_answer, ("and", "or"))
        self.type_of_answer: str = type_of_answer

        self.have_solution: bool = True

    def add_answer(self, answer):
        if not isinstance(answer, Answer) or not self.have_solution:
            return
        if self.type_of_answer == "or":
            if answer.have_solution:
                self.add_solution(answer)
        elif self.type_of_answer == "and":
            if answer.is_empty() or not answer.have_solution:
                self.have_solution = False
            elif answer.solutions:
                self.add_solution(answer)
            elif answer:
                self.add_interval(answer)

    def add_interval(self, answer: dict):
        if not isinstance(answer, dict):
            invalid_type_error(self.add_interval, answer, dict)
        for variable, interval in answer.items():
            if variable in self:
                self[variable] *= interval
            else:
                self[variable] = interval
            if self[variable] is None:
                self.have_solution = False

    def add_solution(self, solution):
        if not isinstance(solution, Answer):
            invalid_type_error(self.add_interval, solution, Answer)
        else:
            bFinded = False
            for solution1 in self.solutions:
                if solution1 == solution:
                    bFinded = True
                    break
            if not bFinded:
                self.solutions.append(solution)

    def is_empty(self) -> bool:
        return not self and not self.solutions

    def reduce(self):
        for answer in self.solutions:
            answer.reduce()
        if self.type_of_answer == "and":
            if not self.solutions:
                return
            for key, value in self.items():
                for answer in self.solutions:
                    if key in answer:
                        answer[key] *= value
                    else:
                        answer[key] = value
                    if answer[key] is None:
                        answer.have_solution = False
            super().clear()
        return

    def __repr__(self):
        result = str()
        for key in sorted(self):
            result += f"{key + ' э ' + str(self[key])}" + '\n'
        for answer in self.solutions:
            if answer.type_of_answer == "and":
                result += "{" + str(answer) + "}\n"
            elif answer.type_of_answer == "or":
                result += "[" + str(answer) + "]\n"
        return '\n' + result

    def clear(self) -> None:
        super().clear()
        self.solutions = list()

    def combinations(self):
        result = Answer()
    def __eq__(self, other):
        if len(self) == len(other) and len(self.solutions) and len(other.solutions):
            for key in self:
                if key not in other:
                    return False
            for solution in self.solutions:
                bFinded = False
                for solution2 in other.solutions:
                    if solution == solution2:
                        bFinded = True
                        break
                if not bFinded:
                    return False
            return True
        return False

    def multiplication(self, other):
        pass
