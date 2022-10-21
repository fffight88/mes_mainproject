from random import *

print("이번주 로또번호를 예측해 봅시다!")
for i in range(6):
    print(int(random()*45) + 1)