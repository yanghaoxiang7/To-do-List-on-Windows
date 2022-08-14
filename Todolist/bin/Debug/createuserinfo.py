import csv
csv_info = open('database/userinfo.csv', 'w')
writer_info = csv.writer(csv_info)
writer_info.writerow(['id', 'password','nickname','sex','birth_year','birth_month','birth_day','alarm','delete_delayed','12_or_24','style','father'])
#info = [  ('1800013066', '123456','黎文浩','boy',2000,6,2,'yes','yes','12','1')]
#writer_info.writerows(info)
csv_info.close()
'''
datapath=info[0][0]+'_data.csv'
csv_data = open(datapath, 'w')
writer_data = csv.writer(csv_data)
writer_data.writerow(['things_ID','thing_to_do','level','12_or_24','deadline_year','deadline_month','deadline_day','deadline_hour','deadline_minute','alarm','alarm_num','finished','delayed','emergency','son_num'])
data=[('1','C#','1',info[0][9],2020,12,31,15,0,'yes',1,'no','no','no',1)]
writer_data.writerows(data)
csv_data.close()

thing_path=info[0][0]+'_'+data[0][0]+'_alarm.csv'
csv_thing = open(thing_path, 'w')
writer_thing = csv.writer(csv_thing)
writer_thing.writerow(['alarm_ID','alarm_year','alarm_month','alarm_day','alarm_hour','alarm_minute','finished'])
thing_data=[('1',2020,12,25,15,0,'no')]
writer_thing.writerows(thing_data)
csv_thing.close()

son_path=info[0][0]+'_'+data[0][0]+'_son.csv'
csv_son=open(son_path,'w')
writer_son=csv.writer(csv_son)
writer_son.writerow(['son_ID','things_to_do','finished'])
son_data=[('1','homework1','no')]
writer_son.writerows(son_data)
csv_son.close()
'''
