for n in $(kubectl get -o=name pvc,,ingress,service,deployment,job,cronjob)
do
    mkdir -p $(dirname $n)
    kubectl get -o=yaml --export $n > $n.yaml
done
