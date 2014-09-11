from ch06_plural_file_of_patterns import build_match_and_apply_functions


def rules(rules_filename):
    with open(rules_filename, encoding='utf-8') as pattern_file:
        for line in pattern_file:
            pattern, search, replace = line.split(None, 3)
            yield build_match_and_apply_functions(pattern, search, replace)


def plural(noun, rules_filename='ch06_plural_file_of_patterns.txt'):
    for matches_rule, apply_rule in rules(rules_filename):
        if matches_rule(noun):
            return apply_rule(noun)


if __name__ == '__main__':
    print(plural('wish'))
    print(plural('Noah'))
    print(plural('victory'))
    print(plural('pay'))
    print(plural('fox'))
    print(plural('word'))
