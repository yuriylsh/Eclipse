import re


def build_match_and_apply_functions(pattern, search, replace):
    def matches_rule(word):
        return re.search(pattern, word)

    def apply_rule(word):
        return re.sub(search, replace, word)

rules = []
with open('ch06_plural_file_of_patterns.txt', encoding='utf-8') as pattern_file:
    for line in pattern_file:
        pattern, search, replace = line.split(None, 3)
        rules.append(build_match_and_apply_functions(pattern, search, replace))

from ch06_plural_list_of_functions import plural
if __name__ == '__main__':
    print(plural('wish'))
    print(plural('Noah'))
    print(plural('victory'))
    print(plural('pay'))
    print(plural('fox'))
    print(plural('word'))
