import itertools

s="воробей"
k=set()
for i in itertools.permutations(s,5):
    b="".join(i)
    if s[0] != "й" and s[len(b)] != "й" and "ей" not in b and "йе" not in b and "й" not in b and "йй" not in b and "ййй" not in b and "йййй" not in b and "ййййй" not in b:
        k.add(b)
print(len(k))