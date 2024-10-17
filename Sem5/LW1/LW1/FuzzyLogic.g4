grammar FuzzyLogic;

kb: fact+ rule*;

fact: ID '=' '{' pair (',' pair)* '}';

rule: ID '~>' ID;

pair: '<' ID ',' FLOAT '>';

ID: [a-zA-Z0-9]+;
FLOAT: [0-9]+ ('.' [0-9]+)?;

LINE_COMMENT: '#' ~[\r\n]* -> skip;
WS: [ \t\r\n]+ -> skip;