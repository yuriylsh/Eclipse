'''
Builds upon ch06_plural_list_of_functions.py.
Abstracts the creation of match_*** and apply_*** functions
'''
import re


def build_match_and_apply_functions(pattern, search, replace):
    def matches_rule(word):
        return re.search(pattern, word)

    def apply_rule(word):
        return re.sub(search, replace, word)
    return (matches_rule, apply_rule)


patterns = \
    (
        ('[sxz]$',                 '$',    'es'),
        ('[^aeioudgkprt]h$',       '$',    'es'),
        ('(qu|[^aeiou]h$',         '$',    'ies'),
        ('$',                      '$',    's')
    )

rules = [build_match_and_apply_functions(patterns, search, replace)
         for (pattern, search, replace) in patterns]

if __name__ == '__main__':
    print(rules)
