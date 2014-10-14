def quicksort(input, start=0, end=None):
    '''
        Performs quick sort.
        Worst case performance: O(n^2)
        Average case performance: O(nlog(n))
        Stable: true
        In-place: true
    '''
    if end is None:
        end = len(input) - 1
    length = end - start + 1
    if length < 2:
        return
    partition_point = partition(input, start, end)
    quicksort(input, start, partition_point - 1)
    quicksort(input, partition_point + 1, length - 1)


def partition(input, start, end):
    if end <= start:
        return start
    pivot = input[start]
    split = start
    for i, val in enumerate(input, start + 1):
        if i > end:
            break
        if input[i] < pivot:
            swap(input, i, split + 1)
            split += 1
    swap(input, start, split)
    return split


def swap(input, i, j):
    tmp = input[i]
    input[i] = input[j]
    input[j] = tmp

if __name__ == '__main__':
    arr = [3, 2, 5, 1, 4]
    print('original', arr)
    quicksort(arr)
    print('sorted', arr)
