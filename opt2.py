import numpy as np
import tensorflow as tf
import math
import matplotlib.pyplot as plt
from scipy.sparse.csgraph._traversal import connected_components

p_count = 5

np.random.seed(0)
tf.random.set_seed(0)

xs = np.random.rand(p_count)
ys = np.random.rand(p_count)

# xs = [0.5, 0.9, 0.7, 0.3, 0.1, 0.4, 0.6]
# ys = [0.9, 0.6, 0.1, 0.1, 0.6, 0.5, 0.5]
# p_count = len(xs)

# iegūst svaru matricu
c = np.zeros(shape=(p_count, p_count))
for i in range(p_count):
    for j in range(p_count):
        c[i][j] = math.sqrt((xs[i]-xs[j])**2+(ys[i]-ys[j])**2)


# nejauša xij matrica, kuru optimizēs
x0 = np.random.rand(p_count, p_count)
np.fill_diagonal(x0, -10)
x0 = (x0 + x0.transpose())/2  # lai ir undirected
x = tf.Variable(x0)


# invertēta vienības matrica
a = np.ones(shape=(p_count, p_count))
np.fill_diagonal(a, 0)


def sample_logistic(shape, eps=1e-20):
    U = tf.random.uniform(shape, minval=eps, maxval=1 - eps)
    U = tf.cast(U, dtype=tf.float64)
    return tf.math.log(U / (1 - U))


# loss funkcija
def loss_fn():

    u = sample_logistic(shape=(p_count, p_count))

    x1 = x + u*0.2

    x1 = tf.sigmoid(x1) * a  # ietver 1. nosacījumu

    cost2 = tf.reduce_mean(tf.abs(1-tf.reduce_sum(x1, 0)))  # 2. nosacījums
    cost3 = tf.reduce_mean(tf.abs(1-tf.reduce_sum(x1, 1)))  # 3. nosacījums
    x1 = x1/(tf.reduce_sum(x1, 0, keepdims=True)+1e-10)
    x1 = x1 / (tf.reduce_sum(x1, 1, keepdims=True) + 1e-10)
    cost1 = tf.reduce_mean(x1*c)  # minimizējamais vienādojums

    cost4 = 0
    F = np.zeros(shape=(p_count, p_count))

    G = []  # G* - sakārto šķautnes pēc svariem
    for i in range(p_count):
        for j in range(p_count):
            if i < j and x1[i][j] > 0:  # undirected
                G.append((x1[i][j], (i, j)))

    G.sort(reverse=True)

    for edge in G:
        i = edge[1][0]  # šķautnes virsotnes
        j = edge[1][1]
        C = connected_components(F)[1]  # F grafa komponentes
        if C[i] != C[j]:  # ja šķautne pieder dažādām F komponentēm
            F[i][j] = x1[i][j]  # pievieno šķautni F grafam
            S = [i, j]  # S kopai pievieno visas šķautnes F komponentē, kurā ir edge
            for k in range(len(C)):
                if k != i and k != j and (C[k] == C[i] or C[k] == C[j]):
                    S.append(k)

            sum = 0  # šo daļu var ātrāk
            for i in range(p_count):
                for j in range(p_count):
                    if i > j and x1[i][j] > 0 and (i in S and j not in S or j in S and i not in S) > 0:
                        sum += x1[i][j]

            cost4 += tf.cast(tf.keras.activations.relu(2 - sum), dtype=tf.float64)
    return cost1 + cost2 + cost3 + cost4


opt = tf.keras.optimizers.Adam(learning_rate=0.01)
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

x_min = x
# loss funkcijas grafiks
min_loss = float('inf')
for i in range(2000):
    tmp = train()
    if tmp < min_loss:
        min_loss = tmp
        x_min = x
    plt.plot([i], [tmp], 'ko')


print(min_loss)
plt.show()
draw_graph(x_min)





