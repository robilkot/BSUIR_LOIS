from antlr4 import InputStream, CommonTokenStream, ParseTreeWalker

from FuzzyLogic.FuzzySet import FuzzySet
from FuzzyLogic.FuzzyValue import FuzzyValue
from FuzzyLogic.Predicate import Predicate
from Parser.LW2Listener import LW2Listener as Listener
from Parser.LW2Parser import LW2Parser as Parser
from Parser.LW2Lexer import LW2Lexer as Lexer


class KBListener(Listener):
    def __init__(self):
        self.implication: FuzzySet = FuzzySet()
        self.rule: Predicate = Predicate()

    def __parse_implication(self, ctx: Parser.ImplicationContext) -> None:
        elements = ctx.elements().getChildren(lambda c: isinstance(c, Parser.ElementContext))
        degrees = ctx.truth_degrees().getChildren(lambda c: isinstance(c, Parser.Membership_degreeContext))

        for pair in zip(elements, degrees):
            element, value = pair[0].getText(), float(pair[1].getText())
            self.implication.add(element, FuzzyValue(value))

    def __parse_rule(self, ctx: Parser.RuleContext) -> None:
        elements2 = [item[0] for item in self.implication]
        elements1 = ctx.elements().getChildren(lambda c: isinstance(c, Parser.ElementContext))

        for i, row in enumerate(ctx.truth_degrees()):
            degrees = row.getChildren(lambda c: isinstance(c, Parser.Membership_degreeContext))

            for pair in zip(elements1, degrees):
                first, second = pair[0].getText(), elements2[i]
                value = FuzzyValue(float(pair[1].getText()))
                self.rule.add((first, second), value)

    def enterKb(self, ctx: Parser.KbContext):
        self.__parse_implication(ctx.implication())
        self.__parse_rule(ctx.rule_())


class Facade:
    def __init__(self, source_text: str):
        self._walker = ParseTreeWalker()
        self._input_stream = InputStream(source_text)
        self._lexer = Lexer(self._input_stream)
        self._stream = CommonTokenStream(self._lexer)
        self._parser = Parser(self._stream)

        self._tree = self._parser.kb()

    def parse(self) -> (FuzzySet, Predicate):
        listener = KBListener()
        self._walker.walk(listener, self._tree)
        return listener.implication, listener.rule


def parse_file(path: str) -> (FuzzySet, Predicate):
    with open(path) as file:
        return Facade(file.read()).parse()
