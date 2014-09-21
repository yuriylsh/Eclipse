import quick_sort
import unittest


class TestMergeSort(unittest.TestCase):

    def test_equal_sizes(self):
        inputs = [[], [1], [1, 2], [1, 2, 3]]
        for input in inputs:
            sorted = input[:]
            quick_sort.quicksort(sorted)
            self.assertEqual(len(input), len(sorted))

    def test_known_results(self):
        input = [4, 2, 1, 5, 3]
        correct = [1, 2, 3, 4, 5]
        self.assert_against_correct(input, correct)

    def test_already_sorted(self):
        input = [1, 2, 3, 4, 5]
        correct = [1, 2, 3, 4, 5]
        self.assert_against_correct(input, correct)

    def test_inversly_sorted(self):
        input = [5, 4, 3, 2, 1]
        correct = [1, 2, 3, 4, 5]
        self.assert_against_correct(input, correct)

    def test_input_with_repeated_items(self):
        input = [1, 3, 2, 1]
        correct = [1, 1, 2, 3]
        self.assert_against_correct(input, correct)
        input = [1, 1, 1, 1]
        correct = [1, 1, 1, 1]
        self.assert_against_correct(input, correct)

    def assert_against_correct(self, input, correct):
        sorted = input[:]
        quick_sort.quicksort(sorted)
        for c, s in zip(correct, sorted):
            self.assertEqual(c, s)


if __name__ == '__main__':
    unittest.main()
