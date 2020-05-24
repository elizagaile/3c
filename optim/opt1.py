import numpy as np
import tensorflow as tf
import math
import matplotlib.pyplot as plt
from scipy.sparse.csgraph import minimum_spanning_tree
from scipy.sparse.csgraph._shortest_path import shortest_path

p_count = 5

np.random.seed(0)
# tf.random.set_seed(0)

xs = np.random.rand(p_count)
ys = np.random.rand(p_count)


# xs = [0.5, 0.9, 0.7, 0.3, 0.1]
# ys = [0.9, 0.6, 0.1, 0.1, 0.6]


# iegūst svaru matricu
c = np.zeros(shape=(p_count, p_count))
for i in range(p_count):
    for j in range(p_count):
        c[i][j] = math.sqrt((xs[i]-xs[j])**2+(ys[i]-ys[j])**2)


# nejauša xij matrica, kuru optimizēs
x0 = np.random.rand(p_count, p_count)
np.fill_diagonal(x0, 0)
x0 = (x0 + x0.transpose())/2  # lai ir undirected
x = tf.Variable(x0)


# invertēta vienības matrica
a = np.ones(shape=(p_count, p_count))
np.fill_diagonal(a, 0)


def path(Pr, i, j):
    path = [j]
    k = j
    while Pr[i, k] != -9999:
        path.append(Pr[i, k])
        k = Pr[i, k]
    return path[::-1]


def sample_logistic(shape, eps=0.45):
    U = tf.random.uniform(shape, minval=eps, maxval=1 - eps)
    U = tf.cast(U, dtype=tf.float64)
    return tf.math.log(U / (1 - U))


# loss funkcija
def loss_fn():

    u = sample_logistic(shape=(p_count, p_count))

    x1 = x + u
    x1 = tf.sigmoid(x1) * a  # ietver 1. nosacījumu

    cost1 = tf.reduce_mean(x1*c)  # minimizējamais vienādojums
    cost2 = tf.reduce_mean(abs(1-tf.reduce_sum(x1, 0)))  # 2. nosacījums
    cost3 = tf.reduce_mean(abs(1-tf.reduce_sum(x1, 1)))  # 3. nosacījums

    mst = minimum_spanning_tree(x1.numpy()*-1).toarray() * -1
    mst = mst + mst.transpose()
    D, Pr = shortest_path(mst, return_predecessors=True)  # 4. nosacījums

    cost4 = 0
    for i in range(p_count):
        for j in range(p_count):
            p = path(Pr, i, j)  # atrod path no i uz j mst kokā
            if len(p) != p_count and i != j :  # jo stingra apakškopa
                par = i  # vecāks
                cost_tmp = x1[i][j]  # pieskaita šķautni, no x1 kura nav kokā
                for k in p:  # iet cauri ceļam kokā
                    cost_tmp += x1[par][k]  # pieskaita atbilstošo šķautni no x1
                    cost_tmp -= 1  # atņem 1, lai kopā atskaitītu |Q|
                    par = k
                cost4 += tf.keras.activations.relu(cost_tmp + 1)

    return cost1 + cost2 + cost3 + cost4


opt = tf.keras.optimizers.Adam(learning_rate=0.05)
def train():
    with tf.GradientTape() as tape:
        loss = loss_fn()
        variables = [x]

        grads = tape.gradient(loss, variables)
        opt.apply_gradients(zip(grads, variables))
    return loss.numpy()


def draw_graph(x1):
    x1 = tf.sigmoid(x1) * a
    plt.scatter(xs, ys, color='k')
    for i in range(p_count):
        for j in range(p_count):
            plt.plot([xs[i], xs[j]], [ys[i], ys[j]], alpha=x1[i][j], color='r', lw='3')
    plt.show()


# loss funkcijas grafiks
min_loss = float('inf')
for i in range(15000):
    tmp = train()
    min_loss = min(min_loss, tmp)
    plt.plot([i], [tmp], 'ko')


print(min_loss)
plt.show()
draw_graph(x)



